﻿using System.Collections.Generic;
using System.Diagnostics;
using Joyride.Platforms;

namespace Joyride.Support
{
    public delegate object ProcessValue(string key, string value);

    public class EntryCreator
    {
        private readonly Component _component;        
        private readonly IDictionary<string, string> _elementMap;
        private readonly ProcessValue _processValue;

        public EntryCreator(Component component, IDictionary<string, string> elementMap)
        {
            _component = component;
            _elementMap = elementMap;
            _processValue = (key, text) => text;
        }

        public EntryCreator(Component component, IDictionary<string, string> elementMap, ProcessValue processText)
        {
            _component = component;
            _elementMap = elementMap;
            _processValue = processText;
        }

        private IDictionary<string, object> AddEntryProperty(IDictionary<string, object> dictonary, string collectionName, int index, string xpath, string key)
        {
            var text = _component.GetTextFromElementWithinCollection(collectionName, index, xpath);
            if (text != null)
            {
                Trace.WriteLine("Found property, " + key + ", with value: " + text);
                var finalValue = _processValue(key, text);
                Trace.WriteLine("Processed property (" + key + ") with value: " + finalValue);
                dictonary.Add(key, finalValue);
            }
            else
                Trace.WriteLine("Unable to find property:  " + key);

            return dictonary;
        }

        public IDictionary<string, object> GetNextEntry(string collectionName, int index)
        {
            IDictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var e in _elementMap)
                dictionary = AddEntryProperty(dictionary, collectionName, index, e.Value, e.Key);

            return dictionary;
        }
    }
}
