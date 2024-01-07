using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLNameClass
{
    public static class NameServiceExtention
    {
        private static readonly string _modelPath =
           Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model", "NameDbModel.zip");
        public static void AddNamePredictEnginPool(this IServiceCollection services)
        {
            services.AddPredictionEnginePool<ModelInput, Modeloutput>()
                .FromFile(_modelPath, true);
        }
    }
}
