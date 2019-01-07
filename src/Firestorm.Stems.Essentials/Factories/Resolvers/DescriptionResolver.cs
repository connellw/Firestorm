using Firestorm.Stems.Definitions;
using Firestorm.Stems.Essentials.Factories.Factories;
using Firestorm.Stems.Fuel.Resolving.Analysis;

namespace Firestorm.Stems.Essentials.Factories.Resolvers
{
    internal class DescriptionResolver : IFieldDefinitionResolver
    {
        public IStemConfiguration Configuration { get; set; }
        public FieldDefinition FieldDefinition { get; set; }

        public void IncludeDefinition<TItem>(EngineImplementations<TItem> implementations)
            where TItem : class
        {
            if (FieldDefinition.Description == null)
                return;

            var description = new AttributeFieldDescription(FieldDefinition.Description);
            implementations.Descriptions.Add(FieldDefinition.FieldName, description);
        }
    }
}