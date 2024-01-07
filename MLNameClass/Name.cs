using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.ML;

namespace MLNameClass
{
    public class Name
    {
        private readonly PredictionEnginePool<ModelInput, Modeloutput> _PredictionEnginePool;
        public Name(PredictionEnginePool<ModelInput, Modeloutput> PredictionEnginePool)
        {
            _PredictionEnginePool = PredictionEnginePool;
        }

        public Modeloutput PredictName(string input)
        {

          return  _PredictionEnginePool.Predict(new ModelInput
            {
                firstname = input,
            });
        }
    }
}
