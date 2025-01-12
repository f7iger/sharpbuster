using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SharpBuster
{
    class WordList
    {
        public static List<string> ler(string arg){
            string wordlist = $"{arg}"; // Redefine to the arg
            StreamReader reader = new StreamReader(wordlist);
            string output = reader.ReadToEnd();
            List<string> words = output.Split('\n').ToList();
            List<string> lista = new List<string>();
            foreach(string word in words){
                lista.Add(word);
            }
            return lista;
        }
    }
    class BruteDirectory
    {
        public static async Task bruteDir(string wordlist, string url){
            Requisicao req = new Requisicao(); //
            List<string> lista = WordList.ler(wordlist); // Read wordlist file
            foreach(string word in lista){
                await req.GetReq($"{url}/{word}"); // Perform HTTP GET request
            }
        }   
    }
    class Program
    {
        public static void help(){
            Console.WriteLine("                    _                      ");
            Console.WriteLine("(_  |_   _. ._ ._  |_)      _ _|_  _  ._   ");
            Console.WriteLine("__) | | (_| |  |_) |_) |_| _>  |_ (/_ |    ");
            Console.WriteLine("               |                         ");
            Console.WriteLine("Usage: sharpbuster [options] [arguments]");
            Console.WriteLine("Options:");
            Console.WriteLine("-h, --help                  Print this help message");
            Console.WriteLine("-v, --version               Print version information");
            //Console.WriteLine("-d, --directory <dir>       Directory to scan (default: current directory)");
            Console.WriteLine("-w, --wordlist <file>       Wordlist file (default: default.txt)");
            Console.WriteLine("-u, --url <url>             Target URL (default: http://localhost)");
            //Console.WriteLine("-t, --threads <num>         Number of concurrent threads (default: 10)");
            //Console.WriteLine("-o, --output <file>         Output file (default: results.txt)");
            Console.WriteLine("\nExamples:");
            Console.WriteLine("\t\t$ dotnet run sharpbuster -u 'http://example.com/' -w 'wordlist'");
            //Console.WriteLine("\t\t$ dotnet run sharpbuster -u 'http://example.com/' -w 'wordlist' -o results.txt");
            //Console.WriteLine("\t\t$ dotnet run sharpbuster -u 'http://example.com/' -w 'wordlist' -t 15");
            //Console.WriteLine("\t\t$ dotnet run sharpbuster -u 'http://example.com/' -d /path/to/directory");
            //Console.WriteLine("\t\t$ dotnet run sharpbuster -u 'http://example.com/' -d /path/to/directory -o results.txt");
            Console.WriteLine("\t\t$ mono sharpbuster -u 'http://example.com/' -w 'wordlist'");
        }
        static int Main(string[] args){
            string url = null;
            string wordlist = null;
            if (args.Length <= 0){
                Console.WriteLine("Invalid Argument, must be at least one");
            }
            try {
                for(int i = 0; i < args.Length; i++){
                    if(args[i] == "-w" || args[i] == "--wordlist"){
                        if(i+1 < args.Length){
                            wordlist = args[i + 1];
                            i++;
                        } else {
                            Console.WriteLine("URL is required");
                            return 1;
                        }
                    } else if(args[i] == "-u" || args[i] == "--url"){
                        if(i+1 < args.Length){
                            url = args[i + 1];
                            i++;
                        } else {
                            Console.WriteLine("Wordlist file is required");
                            return 1;
                        }
                    } else if (args[i] == "-h" || args[i] == "--help" || args[i] == "-help"){
                        help();
                        return 1;
                    } else if (args[i] == "-v" || args[i] == "--version"){
                        Console.WriteLine("Version 1.0.0");
                        return 1;
                    } else {
                        Console.WriteLine($"Unknown option: {args[i]}");
                        help();
                        return 1;
                    }
                }
            } catch (AggregateException ex){
                foreach (var innerException in ex.InnerExceptions) {
                    if (innerException is ArgumentException argumentException) {
                        if (argumentException.Message.Contains("Empty path name is not legal")) {
                            Console.WriteLine("Enter the args");
                        } else {
                            Console.WriteLine($"Erro de argumento: {argumentException.Message}");
                        }
                    } else {
                        Console.WriteLine($"Erro desconhecido: {innerException.Message}");
                    }
                }
            }
            BruteDirectory.bruteDir(wordlist, url).Wait();
            return 0;
        }
    }
}