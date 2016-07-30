using System;
using System.Collections.Generic;
using System.Diagnostics;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Models parsers that processes various sources for quote data.
    /// </summary>
    public abstract class QuoteDataProvider : IQuoteDataProvider
    {
        /// <summary>
        /// Run by the state a quote data provider is in.
        /// </summary>
        protected delegate int RunByState();

        /// <summary>
        /// Triggered when the provider changes status.
        /// </summary>
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        /// <summary>
        /// The state the provider is in.
        /// </summary>
        protected RunByState m_runByState;

        /// <summary>
        /// The provider's status.
        /// </summary>
        protected QuoteProviderStatus m_status;

        /// <summary>
        /// A list of subscribers/listeners to this provider.
        /// </summary>
        protected List<IQuoteDataListener> m_listeners;

        /// <summary>
        /// Initializes a new instance of the <c>QuoteDataProvider</c> class.
        /// </summary>
        public QuoteDataProvider()
        {
            m_runByState = Idle;
            m_status = QuoteProviderStatus.Inactive;
            m_listeners = new List<IQuoteDataListener>();
        }

        /// <summary>
        /// A particular parser's status.
        /// </summary>
        public QuoteProviderStatus Status
        {
            get
            {
                return m_status;
            }
        }

        /// <summary>
        /// The provider's name.
        /// </summary>
        public abstract string ProviderName { get; }

        /// <summary>
        /// Runs a quote data provider.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        public int Run()
        {
            return m_runByState();
        }

        /// <summary>
        /// The inactive state.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        protected int Idle()
        {
            return 10;
        }

        /// <summary>
        /// Changes the status of a quote data provider and notifies listeners.
        /// </summary>
        /// <param name="current">the status to change to.</param>
        protected void ChangeStatus(QuoteProviderStatus current)
        {
            OnStatusChanged(new StatusChangedEventArgs(m_status, current));
            m_status = current;
        }

        /// <summary>
        /// Adds a listener to the parser's subscribers list.
        /// </summary>
        /// <param name="listener">the listener to be added.</param>
        /// <exception cref="System.ArgumentNullException">The input listener is null.</exception>
        public void Subscribe(IQuoteDataListener listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException("listener cannot be null.");
            }

            lock (m_listeners)
            {
                if (!m_listeners.Contains(listener))
                {
                    m_listeners.Add(listener);
                }
            }
        }

        /// <summary>
        /// Removes a listener from the parser's subscribers list.
        /// </summary>
        /// <param name="listener">the listener to be removed.</param>
        /// <exception cref="System.ArgumentNullException">The input listener is null.</exception>
        public void Unsubscribe(IQuoteDataListener listener)
        {
            if (listener == null)
            {
                throw new ArgumentNullException("listener cannot be null.");
            }

            lock (m_listeners)
            {
                if (m_listeners.Contains(listener))
                {
                    m_listeners.Remove(listener);
                }
            }
        }

        /// <summary>
        /// Notifies all listeners that a <c>QuoteMessage</c> object has been successfully received.
        /// </summary>
        /// <param name="message">the <c>QuoteMessage</c> object that has just been received.</param>
        protected virtual void OnQuoteMessageReceived(QuoteMessage message)
        {
            foreach (IQuoteDataListener listener in m_listeners)
            {
                try
                {
                    listener.OnQuoteMessageReceived(message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==>{0} call {1}.OnQuoteMessageReceived() failed, Error: {2}", this, listener.ListenerName, ex.Message);
                }
            }
        }

        /// <summary>
        /// Notifies all listeners that an error has occurred.
        /// </summary>
        /// <param name="ex">the exception that represents the error.</param>
        /// <param name="severe">the severity of this error: true means needs to abort; false means can try to recover.</param>
        protected virtual void OnErrorOccurred(Exception error, bool severe)
        {
            foreach (IQuoteDataListener listener in m_listeners)
            {
                try
                {
                    listener.OnErrorOccurred(error, severe);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("==>{0} call {1}.OnErrorOccurred() failed, Error: {2}", this, listener.ListenerName, ex.Message);
                }
            }
        }

        /// <summary>
        /// Notifies all event handlers to consume the raised event.
        /// </summary>
        /// <param name="e">the raised event.</param>
        protected virtual void OnStatusChanged(StatusChangedEventArgs e)
        {
            EventHandler<StatusChangedEventArgs> handler = StatusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
