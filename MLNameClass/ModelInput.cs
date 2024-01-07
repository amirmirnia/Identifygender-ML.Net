using Microsoft.ML.Data;

namespace MLNameClass
{
    public class ModelInput
    {
        [LoadColumn(1)]
        public string firstname { get; set; }
        [LoadColumn(0), ColumnName("Label")]
        public bool Male { get; set; }
    }
}