﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Firestorm.Engine;
using Firestorm.Engine.Fields;
using Firestorm.Engine.Identifiers;

namespace Firestorm.Fluent.Fuel
{
    internal class FluentEngineContext<TItem> : IEngineContext<TItem>
        where TItem : class
    {
        public FluentEngineContext(FieldImplementationsDictionary<TItem> implementations)
        {
            Fields = new FluentFieldProvider<TItem>(implementations);
        }

        public IDataTransaction Transaction { get; }
        public IEngineRepository<TItem> Repository { get; }
        public IIdentifierProvider<TItem> Identifiers { get; }
        public IFieldProvider<TItem> Fields { get; }
        public IAuthorizationChecker<TItem> AuthorizationChecker { get; }
    }

    internal class FluentFieldProvider<TItem> : IFieldProvider<TItem>
        where TItem : class
    {
        private readonly FieldImplementationsDictionary<TItem> _implementations;

        public FluentFieldProvider(FieldImplementationsDictionary<TItem> implementations)
        {
            _implementations = implementations;
        }

        public IEnumerable<string> GetDefaultNames(int nestedBy)
        {
            throw new NotImplementedException();
        }

        public bool FieldExists(string fieldName)
        {
            return _implementations.ContainsKey(fieldName);
        }

        public IRestResource GetFullResource(string fieldName, IDeferredItem<TItem> item, IDataTransaction dataTransaction)
        {
            throw new NotImplementedException();
        }

        public IFieldReader<TItem> GetReader(string fieldName)
        {
            return _implementations[fieldName].Reader;
        }

        public IFieldWriter<TItem> GetWriter(string fieldName)
        {
            return _implementations[fieldName].Writer;
        }

        public IFieldDescription GetDescription(string fieldName, CultureInfo cultureInfo)
        {
            return _implementations[fieldName].Description;
        }
    }
}
