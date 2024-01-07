using Microsoft.ML.Data;

namespace MLNameClass
{
    public class Modeloutput
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
        public float Probability { get; set; }
    }
}
