using System;
using System.Collections.Generic;

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
        public delegate void State();

        /// <summary>
        /// This event is triggered when the provider changes status.
        /// </summary>
        public event EventHandler<StatusChangedEventArgs> StatusChanged;

        /// <summary>
        /// This field represents the state the provider is in.
        /// </summary>
        protected State m_state;

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
        }

        /// <value>
        /// Property <c>CurrentState</c> represents a particular parser's current state.
        /// </value>
        public State CurrentState
        {
            get
            {
                return m_state;
            }
        }

        /// <value>
        /// Property <c>Status</c> represents a particular parser's status.
        /// Setting a new status triggers a status changed event.
        /// </value>
        public QuoteProviderStatus Status
        {
            get
            {
                return m_status;
            }
            protected set
            {
                OnStatusChanged(new StatusChangedEventArgs(m_status, value));
                m_status = value;
            }
        }

        /// <value>
        /// Property <c>ProviderName</c> represents the provider's name.
        /// </value>
        public abstract string ProviderName { get; }

        /// <summary>
        /// This method is used to run a parser.
        /// </summary>
        public abstract void Run();

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
        /// <param name="msg">the <c>QuoteMessage</c> object that has just been parsed.</param>
        protected void OnMessageParsed(QuoteMessage msg)
        {
            foreach (IQuoteDataListener listener in m_listeners)
            {
                try
                {
                    listener.OnQuoteMessageReceived(msg);
                }
                catch
                {
                }
            }
        }

        ///// <summary>
        ///// This method notifies all listeners that the provider has had a status change.
        ///// </summary>
        ///// <param name="previous">the previous state.</param>
        ///// <param name="current">the current state.</param>
        //protected void OnStatusChanged(QuoteProviderStatus previous, QuoteProviderStatus current)
        //{
        //    foreach (IQuoteDataListener listener in m_listeners)
        //    {
        //        try
        //        {
        //            listener.OnStatusChanged(previous, current);
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}

        /// <summary>
        /// This method notifies all listeners that an error has occurred.
        /// </summary>
        /// <param name="ex">the exception that represents the error.</param>
        /// <param name="severe">the severity of this error: true means needs to abort; false means can try to recover.</param>
        protected void OnErrorOccurred(Exception ex, bool severe)
        {
            foreach (IQuoteDataListener listener in m_listeners)
            {
                try
                {
                    listener.OnErrorOccurred(ex, severe);
                }
                catch
                {
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
