using System;
using Firestorm.Engine.Additives.Writers;
using Firestorm.Engine.Fields;
using Firestorm.Stems.Attributes.Definitions;
using Firestorm.Stems.Fuel.Resolving.Factories;

namespace Firestorm.Stems.Fuel.Essential.Factories
{
    internal class ActionFieldWriterFactory<TItem, TValue> : IFactory<IFieldWriter<TItem>, TItem>
        where TItem : class
    {
        private readonly FieldDefinitionHandlerPart.GetInstanceMethodDelegate _getInstanceLocatorMethod;

        public ActionFieldWriterFactory(FieldDefinitionHandlerPart.GetInstanceMethodDelegate getInstanceLocatorMethod)
        {
            _getInstanceLocatorMethod = getInstanceLocatorMethod;
        }

        public IFieldWriter<TItem> Get(Stem<TItem> stem)
        {
            var instanceMethod = (Action<TItem, TValue>) _getInstanceLocatorMethod.Invoke(stem);
            return new ActionFieldWriter<TItem, TValue>( instanceMethod);
        }
    }
}