using Inetlab.SMPP.Common;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Artnix.Smpp.Client
{
    public sealed class AnMultiMessage : AnMessage, ICollection<AnMessage>, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber">Administrator phone number.</param>
        /// <param name="text"></param>
        public AnMultiMessage(string phoneNumber, string text, Language language = Language.Armenian)
            : base(phoneNumber, text, language)
        {
            administratorPhones = new HashSet<string>();
            clientMessages = new AnSingleMessageCollaction();
        }

        private AnSingleMessageCollaction clientMessages;
        private HashSet<string> administratorPhones;
        private bool disposed;

        public int Count
        {
            get { return clientMessages.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        public void AddAdministratorPhone(string phoneNumber)
        {
            administratorPhones.Add(phoneNumber);
        }

        public IEnumerable<string> GetAdministrators()
        {
            return administratorPhones;
        }

        /// <summary>
        /// Form Clients.
        /// </summary>
        /// <param name="message"></param>
        public void Add(AnMessage message)
        {
            if (!clientMessages.Contains(message.phoneNumber))
                clientMessages.Add(message);
        }

        public void Add(string phoneNumber, string text)
        {
            Add(new AnMessage(phoneNumber, text));
        }

        public bool Remove(AnMessage message)
        {
            bool isContains = Contains(message);
            if (isContains)
                clientMessages.Remove(message);

            return isContains;
        }

        public IEnumerator<AnMessage> GetEnumerator()
        {
            return clientMessages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            clientMessages.Clear();
            administratorPhones.Clear();
        }

        public bool Contains(AnMessage message)
        {
            return clientMessages.Contains(message);
        }

        public void CopyTo(AnMessage[] array, int arrayIndex)
        {
            clientMessages.CopyTo(array, arrayIndex);
        }

        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnDispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Clear();

                    clientMessages = null;
                    administratorPhones = null;
                }
                disposed = true;
            }
        }

        private sealed class AnSingleMessageCollaction : KeyedCollection<string, AnMessage>
        {
            protected override string GetKeyForItem(AnMessage item)
            {
                return item.phoneNumber;
            }
        }
    }

    public class AnMessage
    {
        public AnMessage(string phoneNumber, string text, Language language = Language.Armenian)
        {
            this.phoneNumber = phoneNumber;
            this.text = text;
            this.language = language;
        }

        public readonly string phoneNumber;
        public readonly string text;
        public readonly Language language;

        internal DataCodings DataCoding
        {
            get
            {
                return language == Language.Armenian ? 
                    DataCodings.Class1MEMessageUCS2 : 
                    DataCodings.Default;
            }
        }
    }
}