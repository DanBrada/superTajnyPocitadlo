using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

class MainClass {
  public string[] time = new string[3];
  public int[] score = new int[2];
  public int swstate = 0;
  public bool isRunning = false, runsOffStopwatch = false;
  public static void Main (string[] args) {
    MainClass mc = new MainClass();
    mc.score[0] = 0;
    mc.score[1] = 0;
    Task.Run(() => mc.selfRewritingDisplay(mc) );
    input:
    string readKey = Console.ReadKey().Key.ToString();
    switch (readKey){
      case "A":
        mc.score[0]++;
        goto input;
      case "S":
        mc.score[1]++;
        goto input;
      case "E":
        Console.Clear();
        Environment.Exit(1);
        break;
      case "R":
        mc.score[0] = 0;
        mc.score[1] = 0;
        mc.isRunning = false; 
        mc.swstate = 3;
        mc.runsOffStopwatch = false;
        goto input;
      case "B":
        mc.isRunning = true;
        mc.swstate = 1;
        if(!mc.runsOffStopwatch){
          mc.runsOffStopwatch = true;
          Task.Run(() => mc.stopwatch(mc));
          
        } 
        
        goto input;
      case "N":
        mc.isRunning = false;
        mc.swstate = 2;
        goto input;
      case "M":
        mc.isRunning = false;
        mc.swstate = 3;
        goto input;
      default:
        goto input;
    }
  }
  void stopwatch(MainClass mc){
    Stopwatch sw = new Stopwatch();
    TimeSpan ts = sw.Elapsed;
    int laststate = 0;
    while(runsOffStopwatch){
      if(laststate != swstate){
        if(swstate >= 1 && swstate <= 3){
          switch(swstate){
            case 1: //start
              laststate = swstate;
              //isRunning = true;
              sw.Start();
              break;
            case 2: //pause
              laststate = swstate;
              isRunning = false;
              sw.Stop();
              break;
            case 3: //stop ======> reset
              laststate = swstate;
              isRunning = false;
              sw.Stop();
              sw.Reset();
              goto writeD;
          }
        }
      }
      writeD:
      ts = sw.Elapsed;
      mc.time[0] = ts.Hours.ToString();
      mc.time[1] = ts.Minutes.ToString();
      mc.time[2] = ts.Seconds.ToString();
      //mc.swstate = 0;
    }
  }
  void selfRewritingDisplay(MainClass mc){
    hruška:
    DateTime dt  = DateTime.Now;
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("Scorebord v1 by mineik - github.com/mineikCZ/superTajnyPocitadlo");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write(mc.score[0]);
    Console.ForegroundColor = ConsoleColor.White;
    if(!runsOffStopwatch){
      Console.Write(dt.ToString("       HH:mm:ss      "));
    }else{
      Console.Write("       {0}:{1}:{2}       ", mc.time[0], mc.time[1], mc.time[2]);
    }
    
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(mc.score[1]);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("a - add left; s - add right; r - reset; e - exit; b/m/s - start/pause/stop, reset timer");
    Thread.Sleep(100);
    while("malina"!="hruška") goto hruška;
  }
}