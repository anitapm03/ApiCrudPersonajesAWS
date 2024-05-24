using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiCrudPersonajesAWS.Repositories;
using ApiCrudPersonajesAWS.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiCrudPersonajesAWS;

public class Functions
{
    private RepositoryPersonajes repo;
    public Functions(RepositoryPersonajes repo)
    {
        this.repo = repo;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<IHttpResult> Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        List<Personaje> personajes =
            await this.repo.GetPersonajesAsync();
        return HttpResults.Ok(personajes);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{id}")]
    public async Task<IHttpResult> Find(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        Personaje personaje = await
            this.repo.FindPersonajeAsync(id);
        return HttpResults.Ok(personaje);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/post")]
    public async Task<IHttpResult> Post([FromBody]
        Personaje personaje, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Post' Request");
        Personaje personajeNew = await
            this.repo.CreatePersonajeAsync
            (personaje.Nombre, personaje.Imagen);
        return HttpResults.Ok(personajeNew);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Put, "/put")]
    public async Task<IHttpResult> Put([FromBody]
        Personaje personaje, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Put' Request");
        Personaje personajeNew = await
            this.repo.CreatePersonajeAsync
            (personaje.Nombre, personaje.Imagen);
        return HttpResults.Ok("Todo OK Jose Luis xD");
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Delete, "/delete/{id}")]
    public async Task<IHttpResult> Delete(int id, ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Put' Request");
        await this.repo.DeletePersonajeAsync(id);
        return HttpResults.Ok("Eliminado con exito :P");
    }

}
