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
        protected RunByState m_state;

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
            m_listeners = new List<IQuoteDataListener>();
            m_status = QuoteProviderStatus.Inactive;
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
        /// <returns>Time to sleep in milliseconds till executing next state.</returns>
        public int Run()
        {
            return m_state();
        }

        /// <summary>
        /// Changes the status of a quote data provider and notifies listeners.
        /// </summary>
        /// <param name="next">the status to change to.</param>
        protected void ChangeStatus(QuoteProviderStatus next)
        {
            OnStatusChanged(new StatusChangedEventArgs(m_status, next));
            m_status = next;
        }

        /// <summary>
        /// Adds a listener to the parser's subscribers list.
        /// </summary>
        /// <param name="listener">the listener to be added.</param>
        public void Subscribe(IQuoteDataListener listener)
        {
            if (!m_listeners.Contains(listener))
            {
                m_listeners.Add(listener);
            }
        }

        /// <summary>
        /// Removes a listener from the parser's subscribers list.
        /// </summary>
        /// <param name="listener">the listener to be removed.</param>
        public void Unsubscribe(IQuoteDataListener listener)
        {
            if (m_listeners.Contains(listener))
            {
                m_listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Notifies all listeners that a <c>QuoteMessage</c> object has been successfully parsed.
        /// </summary>
        /// <param name="message">the <c>QuoteMessage</c> object that has just been parsed.</param>
        protected void OnMessageParsed(QuoteMessage message)
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
        protected void OnErrorOccurred(Exception error, bool severe)
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
        /// <param name="ev">the raised event.</param>
        protected virtual void OnStatusChanged(StatusChangedEventArgs ev)
        {
            EventHandler<StatusChangedEventArgs> handler = StatusChanged;
            if (handler != null)
            {
                handler(this, ev);
            }
        }
    }
}
