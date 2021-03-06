using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Firestorm.Engine.Fields;
using Firestorm.Engine.Subs.Context;

namespace Firestorm.Engine.Subs.Handlers
{
    public class SubCollectionFieldReader<TItem, TProperty, TNav> : IFieldReader<TItem>
        where TItem : class
        where TNav : class, new()
        where TProperty : IEnumerable<TNav>
    {
        private readonly Expression<Func<TItem, TProperty>> _navigationExpression;
        private readonly IEngineSubContext<TNav> _engineSubContext;

        public SubCollectionFieldReader(Expression<Func<TItem, TProperty>> navigationExpression, IEngineSubContext<TNav> engineSubContext)
        {
            _navigationExpression = navigationExpression;
            _engineSubContext = engineSubContext;

            Func<IQueryable<TItem>, IQueryable<IEnumerable<TNav>>> selectAllNavFunc = q => q.Select(_navigationExpression).Cast<IEnumerable<TNav>>();
            Replacer = new SubCollectionReplacer<TItem, TNav>(engineSubContext, selectAllNavFunc);
        }

        public Type FieldType => typeof(IEnumerable); // TODO maybe just object ?

        public Expression GetSelectExpression(ParameterExpression itemPram)
        {
            var visitedNavigationExpr =
                (LambdaExpression) new ParameterReplacerVisitor(_navigationExpression.Parameters[0], itemPram).Visit(
                    _navigationExpression);

            LambdaExpression memberInitLambda = SubUtilities.GetMemberInitLambda(_engineSubContext.Fields);

            Type dynamicType = memberInitLambda.ReturnType;
            MethodCallExpression selectMethodExpr = Expression.Call(typeof(Enumerable), "Select",
                new[] {typeof(TNav), dynamicType}, visitedNavigationExpr.Body, memberInitLambda);
            
            return selectMethodExpr;
        }

        public IFieldValueReplacer<TItem> Replacer { get; }
    }
}