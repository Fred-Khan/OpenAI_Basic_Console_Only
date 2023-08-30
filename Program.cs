using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

string? responseStatusCode = null; // Variable to store the response status code

// Get the OpenAI settings from appsettings.json
string openAiApiURL = "https://api.openai.com/v1/completions" ?? string.Empty; // Define the OpenAI API URL
string openAiApiKey = "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" ?? string.Empty; // Define your API key
string prompt = "Generate 3 astronomy multiple-choice quiz questions with correct answers. Format the quiz in valid JSON format. Use the following JSON format but replace single quotes with double quotes: {'Question 1':{'Question':'Sample question?','Options':{'A':'Sample answer 1','B':'Sample answer 2','C':'Sample answer 3','D':'Sample answer 4'},'Answer':'B'}}" ?? string.Empty; // Define the text prompt
string model = "text-davinci-003" ?? string.Empty; // Define the model to use
int max_tokens = 1000; // Define the maximum number of tokens in the response
double temperature = 0.5; // Define the temperature parameter for controlling randomness

var headers = new AuthenticationHeaderValue("Bearer", openAiApiKey); // Create an AuthenticationHeaderValue with the API key

// Data object to hold prompt, model, max_tokens, and temperature
var data = new
{
    prompt,
    model,
    max_tokens,
    temperature
};

string json = JsonConvert.SerializeObject(data); // Serialize the data object into a JSON string

System.Console.WriteLine($"JSON String to send to Open AI API: \n{json}"); // Print the JSON string to the console

using (var client = new HttpClient()) // Create a new HttpClient
{
    client.DefaultRequestHeaders.Authorization = headers; // Set the Authorization header using the API key

    var response = await client.PostAsync(openAiApiURL, new StringContent(json, Encoding.UTF8, "application/json"));
    // Send a POST request to the OpenAI API with the JSON data

    responseStatusCode = response.StatusCode.ToString(); // Get the response status code

    if (response.IsSuccessStatusCode) // If the response is successful (status code 2xx)
    {
        string responseContent = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        Console.WriteLine($"Response Content:\n{responseContent}"); // Print the response content
    }
    else // If the response is not successful
    {
        Console.WriteLine($"\nERROR: Failed to generate the quiz. Status code: {responseStatusCode}");
    }
}
