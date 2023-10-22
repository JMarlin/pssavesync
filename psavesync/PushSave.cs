using System.Data.Common;
using System.Net;
using MemcardRex;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PSaveSync;
using PSaveSync.Data;

namespace psavesync {

    public class PushSave {

        private readonly ILogger _logger;
        private readonly PSaveSyncDbContext _dbContext;

        public PushSave(ILoggerFactory loggerFactory, PSaveSyncDbContext dbContext) =>
            (_logger, _dbContext) = (
                loggerFactory.CreateLogger<PushSave>(),
                dbContext );

        [Function("PushSave")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req) {

            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var card = new ps1card();

            try {

                using(var reader = new StreamReader(req.Body))
                card.openMemoryCardStream(Convert.FromBase64String(reader.ReadToEnd()), false);
            } catch(Exception e) {

                var failResponse = req.CreateResponse(HttpStatusCode.BadRequest);

                failResponse.WriteString($"Bad card: {e.Message}");
                failResponse.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                return failResponse;
            }

            var saveRecord = Enumerable.Range(0, 15)
                .Where(i => card.saveType[i] == 1)
                .Select(SaveRecord.FromCardSlot(card)).First();

            //Add a save
            var save = new Save(card, Enumerable.Range(0, 15).First(i => card.saveType[i] == 1));
            _dbContext.Save.Add(save);
            _dbContext.SaveChanges();

            //Retrieve everything
            var gotSave = _dbContext.Save.First();
            var saveBlocks = gotSave.Blocks.ToArray();


            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
