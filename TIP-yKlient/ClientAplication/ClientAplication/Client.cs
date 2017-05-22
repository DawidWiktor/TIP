﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientAplication
{
    class Client
    {
        private TcpClient _client;

        private StreamReader _sReader;
        private StreamWriter _sWriter;

        private Boolean _isConnected;
        private DiffieHelman diffieHelman;
        Random rnd = new Random();

        public Client(String ipAddress, int portNum)
        {
            _client = new TcpClient();
            _client.Connect(ipAddress, portNum);

            HandleCommunication();

            _client.GetStream().Close();
            _client.Close();
            _client.Dispose();
        }



        public void HandleCommunication()
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.ASCII);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);

            _isConnected = true;
            String sData = null;
            diffieHelman = new DiffieHelman();
            diffieHelman.CreateDH(_sReader, _sWriter, diffieHelman);
            Console.WriteLine("create");
            /*  while (_isConnected) {
                  Console.WriteLine("create");
                  //utworzenie polacenia szyfrowanego


              //komunikacja z serwerem szyfrowana
              Console.WriteLine("po create");
                  //wysylanie wiadomosci sData to string ktory ma zostac wyslany (komunikat SEND)
                  diffieHelman.sendMessage(sData, diffieHelman, _sWriter, "SEND");

                  // nasluchiwanie otrzymywania danych z serwera (po otrzymaniu paczki zwraca stringa
                  String sDataIncomming = diffieHelman.reciveMessage(_sReader, diffieHelman);
                  Console.WriteLine("Otrzymane dane z serwera: " + sDataIncomming);

                  //w przypadku gdy klient wysyla exit zamykamy polaczenie (wylaczamy watek
                  if (sData == "EXIT")
                  {
                      _isConnected = false;
                  }
              }*/
        }

        public void sendMessage(string content)
        {
           
            _isConnected = true;
            String sData = null;
            diffieHelman = new DiffieHelman();
            diffieHelman.sendMessage1(content, diffieHelman, _sWriter);
            String sDataIncomming = diffieHelman.reciveMessage(_sReader, diffieHelman);
            Console.WriteLine("Otrzymane dane z serwera: " + sDataIncomming);
        }
    }
}
