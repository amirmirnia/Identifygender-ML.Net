using Microsoft.ML;
using Microsoft.ML.Calibrators;
using Microsoft.ML.Trainers;
using MLNameClass;

var mlContext = new MLContext(0);

//loadData
Console.WriteLine("--S A H Mirnia--");
Console.WriteLine("--Load Data--");
IDataView Data = mlContext.Data.LoadFromTextFile<ModelInput>(
    path: @"Data\\NameDB.tsv",
    hasHeader: true,
    separatorChar: '\t',
    allowQuoting: true
    );

    //var InputData=mlContext.Data.TrainTestSplit(Data,.2,null,0);


//Pipeline
Console.WriteLine("--Start Pipeline--");

var PipelineTrain = mlContext
    .Transforms
    .Text
    .FeaturizeText(inputColumnName: "firstname", outputColumnName: "Features")
    .Append(mlContext.Transforms.NormalizeMeanVariance("Features"))
    .AppendCacheCheckpoint(mlContext);
    //.Fit(InputDataSplit.TrainSet);


var trainer = mlContext
    .BinaryClassification
    .Trainers
    .LbfgsLogisticRegression();

//trainer
Console.WriteLine("--Start Trainer--");
var trainerPipeline = PipelineTrain.Append(trainer);
ITransformer model = trainerPipeline.Fit(Data);

//var transformdata = PipelineTrain.Transform(InputData.TrainSet);

//ITransformer model = trainer.Fit(transformdata);


//test data

EvaluateData(mlContext, model, Data);

//Save Model
Console.WriteLine("--Save Model--");

if (!Directory.Exists("Model"))
{
    Directory.CreateDirectory("Model");
}

mlContext.Model.Save(model, Data.Schema, @"Model\\NameDbModel.zip");
Console.WriteLine("--Done--");




//var Datapreparepiplinefile = @"Model\\Datapreparepipline.zip";

//mlcontext.Model.Save(inputDataPrepare, trainDataView.Schema, Datapreparepiplinefile);

//var ReTModel = RetrainModel(modelFile, Datapreparepiplinefile);

//var compliteretrainpipline = inputDataPrepare.Append(ReTModel);
//Console.WriteLine("Save Retrain Model");
//var Retrainmodelfile = @"Model\\AggressionScorerRetranedModel.zip";
//mlcontext.Model.Save(compliteretrainpipline, trainDataView.Schema, Retrainmodelfile);
//Console.WriteLine("Model save {0}", Retrainmodelfile);
//EvaluateData(mlcontext, compliteretrainpipline, inputDataPrepare.Transform(InputDataSplit.TestSet));





//ITransformer RetrainModel(string model, string inputDataPrepare)
//{
//    MLContext mlcontext = new MLContext(0);
//    ITransformer pretrainmodel = mlcontext.Model.Load(model, out _);
//    var oretraminModelParameter = ((ISingleFeaturePredictionTransformer<CalibratedModelParametersBase<LinearBinaryModelParameters, PlattCalibrator>>)
//        pretrainmodel).Model.SubModel;//استخراج هوش یاد گیری مودل

//    var datafile = @"Data\preparedInput.tsv";
//    DataPreparer.CreatePreparedDataFile(datafile, true);

//    IDataView trainDataView = mlcontext.Data.LoadFromTextFile<ModelInput>(
//        path: datafile,
//        hasHeader: true,
//        separatorChar: '\t',
//        allowQuoting: true
//        );


//    ITransformer datapreparepipline = mlcontext.Model.Load(inputDataPrepare, out _);
//    var newdata = datapreparepipline.Transform(trainDataView);

//    var returnModel =
//        mlcontext.BinaryClassification.Trainers.LbfgsLogisticRegression().Fit(newdata, oretraminModelParameter);

//    return returnModel;

//}






void EvaluateData(MLContext mlcontext, ITransformer TrainerData, IDataView TestData)
{
    Console.WriteLine();
    Console.WriteLine("--Evaluate BinaryClassification--");
    Console.WriteLine();

    var predictdata = TrainerData.Transform(TestData);
    var Metrix = mlcontext.BinaryClassification.Evaluate(predictdata);
    Console.WriteLine($"Accuracy:{ Metrix.Accuracy}");
    Console.WriteLine("Confusion Matrix");
    Console.WriteLine();
    Console.WriteLine(Metrix.ConfusionMatrix.GetFormattedConfusionTable());
}