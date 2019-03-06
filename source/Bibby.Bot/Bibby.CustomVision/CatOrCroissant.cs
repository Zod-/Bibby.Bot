using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Extensions.Options;

namespace Bibby.CustomVision
{
    public class CatOrCroissant : ICatOrCroissant
    {
        private readonly CustomVisionPredictionClient _predictionClient;
        private readonly CustomVisionOptions _options;

        public CatOrCroissant(HttpClient httpClient, IOptions<CustomVisionOptions> options)
        {
            _options = options.Value;
            _predictionClient = new CustomVisionPredictionClient(httpClient, false)
            {
                ApiKey = _options.PredictionKey,
                Endpoint = _options.EndPoint
            };
        }

        public async Task<CatOrCroissantPrediction> CatOrCroissantAsync(Stream image)
        {
            var predictions = await _predictionClient.PredictImageAsync(_options.ProjectId, image);
            return CreatePrediction(predictions);
        }

        public async Task<CatOrCroissantPrediction> CatOrCroissantAsync(string url)
        {
            var predictions = await _predictionClient.PredictImageUrlAsync(_options.ProjectId, new ImageUrl(url));
            return CreatePrediction(predictions);
        }

        private static CatOrCroissantPrediction CreatePrediction(ImagePrediction predictions)
        {
            var catOrCroissant = new CatOrCroissantPrediction();
            foreach (var prediction in predictions.Predictions)
            {
                switch (prediction.TagName)
                {
                    case "Cat":
                        catOrCroissant.Cat = prediction.Probability;
                        break;
                    case "Croissant":
                        catOrCroissant.Croissant = prediction.Probability;
                        break;
                }
            }

            return catOrCroissant;
        }
    }

    public class CustomVisionOptions
    {
        public string PredictionKey { get; set; }
        public string TrainingKey { get; set; }
        public string EndPoint { get; set; }
        public Guid ProjectId { get; set; }
    }
}
