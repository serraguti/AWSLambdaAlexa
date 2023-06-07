using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
//[assembly: LambdaSerializer
//    (typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

[assembly: LambdaSerializerAttribute
    (typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda3;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public SkillResponse 
        FunctionHandler(SkillRequest input, ILambdaContext context)
    {
        SkillResponse response = new SkillResponse();
        response.Response = new ResponseBody();
        response.Response.ShouldEndSession = false;
        IOutputSpeech innerResponse = null;
        ILambdaLogger log = context.Logger; 
        log.LogLine($"Skill Request Object:" + JsonConvert.SerializeObject(input));

        if (input.GetRequestType() == typeof(LaunchRequest))
        {
            innerResponse = new PlainTextOutputSpeech();
            (innerResponse as PlainTextOutputSpeech).Text = 
                "Estoy en Alexa con C#!!, dime cosas y hazme preguntas";
        }else if (input.GetRequestType() == typeof(IntentRequest))
        {
            var intentRequest = (IntentRequest)input.Request;
            if (intentRequest.Intent.Name == "preguntastontas")
            {
                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text =
                    "Mañana ENTRA DE TODO, Cognito, Alexa...";
            }
            else
            {
                innerResponse = new PlainTextOutputSpeech();
                (innerResponse as PlainTextOutputSpeech).Text =
                    "Ni idea de lo que me hablas";
            }
        }
        else
        {
            innerResponse = new PlainTextOutputSpeech();
            (innerResponse as PlainTextOutputSpeech).Text =
                "Ni idea de lo que me hablas";
        }

        response.Response.OutputSpeech = innerResponse;
        response.Version = "1.0";
        return response;
    }
}
