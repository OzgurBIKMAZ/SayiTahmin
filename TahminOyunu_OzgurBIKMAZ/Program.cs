using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TahminOyunu_OzgurBIKMAZ
{
    class Program
    {
        static void Main(string[] args)
        {
            char secimDevam = 'e';
            int sayiOyuncu = 0;
            int sayiTahmin = 0;
            int sayiTutulan = 0;
            int sayiZorluk = 0;
            int sayiTahminHak = 0;
            Random r = new Random();

            do
            {
                if (secimDevam.Equals('e'))                 //YENİ OYUN DÖNGÜSÜ
                {
                    bool kontrolOyuncuSayi = false;
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("************************************************\n" +
                                      "*                  Oyun Başlıyor...            *\n" +
                                      "************************************************\n");
                    Console.ResetColor();

                    for (; !kontrolOyuncuSayi;)
                    {
                        Console.Write("Oyuncu Sayısını Giriniz : ");
                        kontrolOyuncuSayi = int.TryParse(Console.ReadLine(), out sayiOyuncu);   // Oyuncu sayısının geçerliği
                        if (kontrolOyuncuSayi == false)                                           // kontrol edilir
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\nHATA!!! Lütfen geçerli bir oyuncu sayısı giriniz.");
                            Console.ResetColor();
                        }

                    }

                    double[] diziOyuncuPuan = new double[sayiOyuncu];
                    string[] diziOyuncuAd = new string[sayiOyuncu];

                    for (int i = 0; i < sayiOyuncu; i++)   //OYUNCU DÖNGÜSÜ
                    {
                        Console.Write($"<{i + 1}>. Oyuncu Adı : ");
                        diziOyuncuAd[i] = Console.ReadLine();

                        bool kontrolZorluk = false;
                        for (; !kontrolZorluk;)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("\n[1].KOLAY (0 - 10) : 'Tam Puan = 10' x3");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.WriteLine("[2].ORTA  (0 - 20) : 'Tam Puan = 20' x5");
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("[3].ZOR   (0 - 30) : 'Tam Puan = 30' x7");
                            Console.ResetColor();
                            Console.Write("Bir zorluk seviyesi seçiniz : ");

                            kontrolZorluk = int.TryParse(Console.ReadLine(), out sayiZorluk);// Zorluk Seviyesi Seçimi geçerliliği kontrol edilir
                            Console.WriteLine();

                            if (sayiZorluk == 1)
                            {
                                Console.WriteLine($"{diziOyuncuAd[i]} : KOLAY seviyeyi seçti. 3 Tahmin Hakkı Var.");
                                sayiTutulan = r.Next(1, 10);
                                sayiTahminHak = 3;
                            }
                            else if (sayiZorluk == 2)
                            {
                                Console.WriteLine($"{diziOyuncuAd[i]} : ORTA seviyeyi seçti. 5 Tahmin Hakkı Var.");
                                sayiTutulan = r.Next(1, 20);
                                sayiTahminHak = 5;
                            }
                            else if (sayiZorluk == 3)
                            {
                                Console.WriteLine($"{diziOyuncuAd[i]} : ZOR seviyeyi seçti. 7 Tahmin Hakkı Var.");
                                sayiTutulan = r.Next(1, 30);
                                sayiTahminHak = 7;
                            }
                            else
                            {
                                kontrolZorluk = false;  //Belirlenen Zorluklardan Birinin Seçilmeme Durumu
                            }

                            if (kontrolZorluk == false)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\nHATA!!! Lütfen geçerli bir zorluk seviyesi giriniz.");
                                Console.ResetColor();
                            }
                        }

                        for (int sayacTahmin = 1; ; sayacTahmin++) // TAHMİN DÖNGÜSÜ 
                        {                                          // [Sayı/0] hatasını engellemek için sayac 1'den başlar

                            bool kontrolTahmin = false;
                            for (; !kontrolTahmin;)
                            {
                                Console.Write($"'{diziOyuncuAd[i]}' [{sayacTahmin}]. Tahmini : ");
                                kontrolTahmin = int.TryParse(Console.ReadLine(), out sayiTahmin);   // Tamsayı dışında tahmin
                                if (kontrolTahmin == false)                                           // girilmesini engeller
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("\nHATA!!! Lütfen bir tam sayı giriniz.");
                                    Console.ResetColor();
                                }

                            }

                            if (sayiTahmin == sayiTutulan)
                            {
                                Console.WriteLine($"Tebrikler {sayacTahmin}. Denemede Buldunuz");
                                diziOyuncuPuan[i] = Math.Round((sayiZorluk * 10.0) / sayacTahmin, 2); // virgulden sonra 2 basamak
                                break;
                            }
                            else
                            {
                                if (sayiTahminHak - sayacTahmin > 0)
                                {
                                    Console.WriteLine($"Yanlış Tahmin. Tekrar Deneyiniz. Kalan Hak = {sayiTahminHak - sayacTahmin}");
                                }
                                else
                                {
                                    Console.WriteLine($"\nTahmin Hakkınız Bitti. Sayı : {sayiTutulan}\n");
                                    diziOyuncuPuan[i] = 0;
                                    break;
                                }

                            }

                        }
                        Console.WriteLine("\n******************************************\n" +
                                  $"*  {"'" + diziOyuncuAd[i],15}'   puanı : {diziOyuncuPuan[i],-10} *\n" +
                                          "******************************************\n");
                    }

                    double geciciPuan = 0;
                    string geciciAd = "";
                    Console.WriteLine();

                    for (int m = 0; m < sayiOyuncu; m++) // Puanlarla birlikte adları da sıralar. Puan-Ad karışıklığını önler
                    {
                        for (int n = m + 1; n < sayiOyuncu; n++)
                        {
                            if (diziOyuncuPuan[m] < diziOyuncuPuan[n])
                            {
                                geciciPuan = diziOyuncuPuan[m];
                                diziOyuncuPuan[m] = diziOyuncuPuan[n];
                                diziOyuncuPuan[n] = geciciPuan;

                                geciciAd = diziOyuncuAd[m];
                                diziOyuncuAd[m] = diziOyuncuAd[n];
                                diziOyuncuAd[n] = geciciAd;
                            }

                        }
                    }

                    Console.WriteLine(">>>>>>>>>   OYUN BİTTİ   <<<<<<<<<");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("*********************************\n*");
                    for (int i = 0; i < sayiOyuncu; i++)            //PUAN TABLOSU DÖNGÜSÜ : Ekrana Renkli Yazım
                    {
                        Console.BackgroundColor = (ConsoleColor)(i % 5);
                        Console.Write($"[{i + 1,2}] {diziOyuncuAd[i],15} {diziOyuncuPuan[i],-10}");

                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("*\n*");
                    }
                    Console.WriteLine("********************************");
                    Console.ResetColor();

                    //********************************************
                    //>>>>>>>    Puan Tablosu Müziği    <<<<<<<<<<
                    Console.Beep(300, 500);
                    Thread.Sleep(50);
                    Console.Beep(300, 500);
                    Thread.Sleep(50);
                    Console.Beep(300, 500);
                    Thread.Sleep(50);
                    Console.Beep(250, 500);
                    Thread.Sleep(50);
                    Console.Beep(350, 250);
                    Console.Beep(300, 500);
                    Thread.Sleep(50);
                    Console.Beep(250, 500);
                    Thread.Sleep(50);
                    Console.Beep(350, 250);
                    Console.Beep(300, 500);
                    Thread.Sleep(50);
                    //**********************************************

                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Hatalı Giriş Yaptınız!!!"); // 'h', 'e' ya da ' ' giriş kontrolü yapılır
                    Console.ResetColor();

                }

                Console.Write("\nYeni Oyun Kurulsun mu ? (E/H) : ");
                char.TryParse(Console.ReadLine().ToLower(), out secimDevam); //char dışında girişleri kontrol eder
                                                                             // false dönerse secimDevam = ' ' olur.

            } while (!secimDevam.Equals('h'));

            Console.WriteLine("\nYine Bekleriz...");

            Console.ReadLine();
        }
    }
}
