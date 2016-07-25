using System;
using System.Collections.Generic;
using System.Diagnostics;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Class <c>QuoteDataProvider</c> models parsers that processes various sources for quote data.
    /// </summary>
    public abstract class QuoteDataProvider : IQuoteDataProvider
    {
        /// <summary>
        /// This delegate represents a state that a particular parser might be in.
        /// </summary>
        protected delegate int RunByState();

        /// <summary>
        /// This event is triggered when the provider changes status.
        /// </summary>
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        /// <summary>
        /// This field represents the state the provider is in.
        /// </summary>
        protected RunByState m_state;

        /// <summary>
        /// This field represents the provider's status.
        /// </summary>
        protected QuoteProviderStatus m_status;

        /// <summary>
        /// This field represents a list of subscribers/listeners to this provider.
        /// </summary>
        protected List<IQuoteDataListener> m_listeners;

        /// <summary>
        /// This constructor does nothing specific.
        /// </summary>
        public QuoteDataProvider()
        {
            m_listeners = new List<IQuoteDataListener>();
            m_status = QuoteProviderStatus.Inactive;
        }

        /// <summary>
        /// Property <c>Status</c> represents a particular parser's status.
        /// Setting a new status triggers a status changed event.
        /// </summary>
        public QuoteProviderStatus Status
        {
            get
            {
                return m_status;
            }
        }

        /// <summary>
        /// Property <c>ProviderName</c> represents the provider's name.
        /// </summary>
        public abstract string ProviderName { get; }

        /// <summary>
        /// This method is used to run a quote data provider.
        /// </summary>
        /// <returns>Time to sleep in milliseconds till executing next state.</returns>
        public int Run()
        {
            return m_state();
        }

        /// <summary>
        /// Changes the status of a quote data provider and notifies listeners.
        /// </summary>
        /// <param name="next"></param>
        protected void ChangeStatus(QuoteProviderStatus next)
        {
            OnStatusChanged(new StatusChangedEventArgs(m_status, next));
            m_status = next;
        }

        /// <summary>
        /// This method adds a listener to the parser's subscribers list.
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
        /// This method removes a listener from the parser's subscribers list.
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
        /// This method notifies all listeners that a <c>QuoteMessage</c> object has been successfully parsed.
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
        /// This method notifies all listeners that an error has occurred.
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
        /// This method notifies all event handlers to consume the raised event.
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
