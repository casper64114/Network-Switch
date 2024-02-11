using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose a network adapter:");
        Console.WriteLine("1. Ethernet");
        Console.WriteLine("2. WiFi");
        Console.WriteLine("3. Local Area Connection 2");
        Console.WriteLine("4. Enter name or IP");

        int choice;
        if(!int.TryParse(Console.ReadLine(), out choice)) {
            Console.WriteLine("Invalid choice");
            return;
        }

        string adapterName;
        switch(choice)
        {
            case 1:
                adapterName = "Ethernet";
                break;
            case 2: 
                adapterName = "WiFi";
                break;
            case 3:
                adapterName = "Local Area Connection 2";
                break;
            case 4:
                Console.Write("Enter name or IP: ");
                adapterName = Console.ReadLine();
                break;
            default:
                Console.WriteLine("Invalid choice");
                return;
        }

        Console.WriteLine();
        Console.Write("Enter full path to app: ");
        string appPath = Console.ReadLine();

        NetworkInterface adapter = GetAdapterByName(adapterName);
        if (adapter == null)
        {
            Console.WriteLine($"Adapter {adapterName} not found");
            return;
        }

        Console.WriteLine($"\nLaunching {appPath} on {adapterName}...");
        
        SetTemporaryIpAddress(adapter, "192.168.1.101");
        LaunchApp(appPath);
        ResetAdapter(adapter);
        
        Console.WriteLine("\nDone!");
    }

    static NetworkInterface GetAdapterByName(string name)
    {
        return NetworkInterface.GetAllNetworkInterfaces()
            .FirstOrDefault(a => a.Name == name); 
    }

    static void SetTemporaryIpAddress(NetworkInterface adapter, string ip)
    {
        IPInterfaceProperties ipProps = adapter.GetIPProperties();
        ipProps.UnicastAddresses[0].Address = IPAddress.Parse(ip);
        adapter.SetIPInterfaceProperties(ipProps);
    }

    static void ResetAdapter(NetworkInterface adapter)
    {
        adapter.SetIPInterfaceProperties(new IPInterfaceProperties());
    }

    static void LaunchApp(string path)
    {
        Process.Start(path);
    }
}
