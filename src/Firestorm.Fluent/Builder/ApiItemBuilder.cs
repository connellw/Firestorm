﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Firestorm.Fluent
{
    public class ApiItemBuilder<TItem> : IApiItemBuilder
        where TItem : class
    {
        internal IList<IApiFieldBuilder<TItem>> Fields { get; } = new List<IApiFieldBuilder<TItem>>();
        internal IList<IApiIdentifierBuilder<TItem>> Identifiers { get; } = new List<IApiIdentifierBuilder<TItem>>();

        public ApiIdentifierBuilder<TItem, TIdentifier> Identifier<TIdentifier>(Expression<Func<TItem, TIdentifier>> expression)
        {
            var identifierBuilder = new ApiIdentifierBuilder<TItem, TIdentifier>();
            Identifiers.Add(identifierBuilder);
            return identifierBuilder;
        }

        public ApiFieldBuilder<TItem, TField> Field<TField>(Expression<Func<TItem, TField>> expression)
        {
            var fieldBuilder = new ApiFieldBuilder<TItem, TField>(expression);
            Fields.Add(fieldBuilder);
            return fieldBuilder;
        }

        public IFluentCollectionCreator GetCollectionCreator(IFluentCollectionCreatorCreator fluentCreator)
        {
            return fluentCreator.GetCollectionCreator<TItem>(this);
        }

        public Type ItemType => typeof(TItem);
    }
}