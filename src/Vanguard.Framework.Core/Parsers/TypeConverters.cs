namespace Vanguard.Framework.Core.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// The type converters.
    /// </summary>
    internal class TypeConverters : ReadOnlyDictionary<Type, TypeConverterBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeConverters"/> class.
        /// </summary>
        public TypeConverters()
            : base(GetDefaultConverters())
        {
        }

        private static IDictionary<Type, TypeConverterBase> GetDefaultConverters()
        {
            return new Dictionary<Type, TypeConverterBase>
            {
                { typeof(bool), new BooleanConverter() },
                { typeof(bool?), new BooleanConverter() },
                { typeof(int), new Int32Converter() },
                { typeof(int?), new Int32Converter() },
                { typeof(short), new Int16Converter() },
                { typeof(short?), new Int16Converter() },
                { typeof(long), new Int64Converter() },
                { typeof(long?), new Int64Converter() },
                { typeof(decimal), new DecimalConverter() },
                { typeof(decimal?), new DecimalConverter() },
                { typeof(float), new FloatConverter() },
                { typeof(float?), new FloatConverter() },
                { typeof(double), new DoubleConverter() },
                { typeof(double?), new DoubleConverter() },
                { typeof(string), new StringConverter() },
                { typeof(char), new CharConverter() },
                { typeof(char?), new CharConverter() },
                { typeof(Guid), new GuidConverter() },
                { typeof(Guid?), new GuidConverter() }
            };
        }
    }
}
