using System;
using System.Net;
using System.IO; 


class TestClient
{
  static void Main(string[] args)
  {
     Console.WriteLine( "test .NET proxy w/ default network credentials" );

     string winProxyAddress = Environment.GetEnvironmentVariable("WIN_PROXY");
     if( winProxyAddress == null )
     {
        Console.WriteLine( "*** error - WIN_PROXY env varibale missing, please set e.g.:");
        Console.WriteLine( "  $ set WIN_PROXY=http://proxy.bigcorp:57416");
        Environment.Exit( 1 );  // todo: check if 0 is ok and 1 is error etc.
    }

     Console.WriteLine( "  WIN_PROXY=>>"+ winProxyAddress+"<<" );
     WebProxy proxy = new WebProxy( winProxyAddress );
     proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

     WebClient client = new WebClient();
     client.Proxy = proxy;

     Console.WriteLine( "  before OpenRead" );
     StreamReader reader = new StreamReader( client.OpenRead( "http://www.orf.at" ));
     Console.WriteLine( "  after OpenRead" );
     
     string str = null;
     while( (str=reader.ReadLine())!= null )
       Console.WriteLine(str);
  }
}
