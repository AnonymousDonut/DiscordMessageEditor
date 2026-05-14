using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.IO;

Console.WriteLine(@" ________  ___  ________  ________  ________  ________  ________          _____ ______   _______   ________   ________  ________  ________  _______           _______   ________  ___  _________  ________  ________     
|\   ___ \|\  \|\   ____\|\   ____\|\   __  \|\   __  \|\   ___ \        |\   _ \  _   \|\  ___ \ |\   ____\ |\   ____\|\   __  \|\   ____\|\  ___ \         |\  ___ \ |\   ___ \|\  \|\___   ___\\   __  \|\   __  \    
\ \  \_|\ \ \  \ \  \___|\ \  \___|\ \  \|\  \ \  \|\  \ \  \_|\ \       \ \  \\\__\ \  \ \   __/|\ \  \___|_\ \  \___|\ \  \|\  \ \  \___|\ \   __/|        \ \   __/|\ \  \_|\ \ \  \|___ \  \_\ \  \|\  \ \  \|\  \   
 \ \  \ \\ \ \  \ \_____  \ \  \    \ \  \\\  \ \   _  _\ \  \ \\ \       \ \  \\|__| \  \ \  \_|/_\ \_____  \\ \_____  \ \   __  \ \  \  __\ \  \_|/__       \ \  \_|/_\ \  \ \\ \ \  \   \ \  \ \ \  \\\  \ \   _  _\  
  \ \  \_\\ \ \  \|____|\  \ \  \____\ \  \\\  \ \  \\  \\ \  \_\\ \       \ \  \    \ \  \ \  \_|\ \|____|\  \\|____|\  \ \  \ \  \ \  \|\  \ \  \_|\ \       \ \  \_|\ \ \  \_\\ \ \  \   \ \  \ \ \  \\\  \ \  \\  \| 
   \ \_______\ \__\____\_\  \ \_______\ \_______\ \__\\ _\\ \_______\       \ \__\    \ \__\ \_______\____\_\  \ ____\_\  \ \__\ \__\ \_______\ \_______\       \ \_______\ \_______\ \__\   \ \__\ \ \_______\ \__\\ _\ 
    \|_______|\|__|\_________\|_______|\|_______|\|__|\|__|\|_______|        \|__|     \|__|\|_______|\_________\\_________\|__|\|__|\|_______|\|_______|        \|_______|\|_______|\|__|    \|__|  \|_______|\|__|\|__|
                  \|_________|                                                                       \|_________\|_________|                                                                                             
                                                                                                                                                                                                                         
                                                                                                                                                                                                                         ");
string authtxtpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "authorization.txt");
string auth = null;
if (!File.Exists(authtxtpath))
{
    using (FileStream fs = File.Create(authtxtpath))
    {
        Console.WriteLine("It appears you havent used this app yet so i have created the txt file that you need to put in your discord authorization in order to use it");
        Console.ReadLine();
        Environment.Exit(0);
    }
}
else
{
    Console.WriteLine("You set the authorization now so we can continue.");
    auth = File.ReadAllText(authtxtpath);
}

Console.WriteLine("Enter message id to continue: ");
var messageid =  Console.ReadLine();

Console.WriteLine("Noted. Enter what the channel id is to continue: ");
var ChannelID = Console.ReadLine();

Console.WriteLine("Wrote that down. Enter what the new message should be: ");
var newMessage = Console.ReadLine();

using HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", auth);

var payload = new 
{
    
    mobile_network_type = "unknown",
    content = newMessage,
    nonce = messageid,
    tts = false,
    flags = 0
};

string jsonString = JsonSerializer.Serialize(payload);
var content = new StringContent(jsonString, Encoding.UTF8, "application/json");


HttpResponseMessage response = await client.PostAsync($"https://discord.com/api/v9/channels/{ChannelID}/messages", content);
if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Edited message");
}

Console.ReadLine();