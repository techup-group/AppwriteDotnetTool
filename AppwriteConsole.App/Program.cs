using AppwriteClient;
using AppwriteClient.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static AppwriteClient.DTOs.AttributeDTO;
using Helpers;

internal class Program
{
    private static void Main()
    {
        ConfigurationHelper config = new ConfigurationHelper();

        config.PrintConfiguration();


        /*
        * TODO: Validate that a database doesnt already exist
        * TODO: Create Backup
        * TODO: Create Database
        * TODO: Create Collections and Attributes based on file schema
        * TODO: Add logging for process
        * TODO: Add required user prompts
        * TODO: Write unit test for code
        */
    }
}