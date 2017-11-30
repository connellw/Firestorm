using System;
using System.Linq.Expressions;
using Firestorm.Data;
using Firestorm.Engine.Subs.Context;

namespace Firestorm.Engine.Subs.Handlers
{
    public class SubItemResourceGetter<TItem, TNav> : SubItemFieldHandlerBase<TItem, TNav>, IFieldResourceGetter<TItem>
        where TItem : class
        where TNav : class, new()
    {
        public SubItemResourceGetter(Expression<Func<TItem, TNav>> navigationExpression, IEngineSubContext<TNav> engineSubContext)
            : base(navigationExpression, engineSubContext)
        { }

        public IRestResource GetFullResource(IDeferredItem<TItem> item, IDataTransaction dataTransaction)
        {
            return GetQueriedEngineItem(item, dataTransaction);
        }
    }
}