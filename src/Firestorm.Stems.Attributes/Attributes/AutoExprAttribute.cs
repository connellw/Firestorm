using System;

namespace Firestorm.Stems.Attributes.Attributes
{
    /// <summary>
    /// Indicates that the property should be interpreted as an Expression by its name and type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoExprAttribute : Attribute
    {
    }
}