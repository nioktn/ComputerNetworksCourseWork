using System;
using System.Collections.Generic;
using System.Linq;
using static ConsoleApp1.Program.Operations;

namespace ConsoleApp1
{
    class Program
    {
        public static int composeSubnetCounter = 5;
        const string beginning = "213.78";
        static void Main(string[] args)
        {
            var numsOfHostsInSubnets = new int[] { 1440, 1260, 1560, 1120, 620, 688, 914, 856, 624, 316, 466, 454, 290, 436, 382, 338, 316, 490 };
            var nets = new List<Net>();

            for (int i = 1; i <= numsOfHostsInSubnets.Length; i++)
            {
                Console.WriteLine($"{string.Empty.PadRight(90, '-')}\n\n");
                var subnetBinaryOctet3Part1 = Convert.ToString(i, 2);
                var clearDefaultSubnetByOctetDecimal = Convert.ToInt32((subnetBinaryOctet3Part1 + "000"), 2);

                var binFullHost = FixBinaryView(Convert.ToString(numsOfHostsInSubnets[i - 1], 2), 11);
                var subNetOctet3Part2 = binFullHost.Substring(0, 3);
                var hostOctet4 = binFullHost.Substring(3);

                var hostOctet4Decimal = Convert.ToInt32(hostOctet4, 2);
                var subNetOctet3DecimalFull = Convert.ToInt32((subnetBinaryOctet3Part1 + subNetOctet3Part2), 2);

                Console.WriteLine(
               $@"{i}) Number of subnets: {numsOfHostsInSubnets[i - 1]}
                    Net address:       {beginning}.0.0                                              
                    Subnet address:    {beginning}.{clearDefaultSubnetByOctetDecimal}.0  | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_000.{FixBinaryView("0", 8)}
                    Lower border:      {beginning}.{clearDefaultSubnetByOctetDecimal}.1  | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_000.{FixBinaryView("1", 8)}
                    Top border:        {beginning}.{subNetOctet3DecimalFull}.{hostOctet4Decimal} | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_{subNetOctet3Part2}.{FixBinaryView(hostOctet4, 8)}");
                nets.Add(new Net() { subnetBinaryOctet3Part1 = subnetBinaryOctet3Part1, subNetOctet3DecimalFull = subNetOctet3DecimalFull, subNetOctet3Part2 = subNetOctet3Part2, hostOctet4 = hostOctet4, hostOctet4Decimal = hostOctet4Decimal, numOfHosts = numsOfHostsInSubnets[i - 1] });
            }

            ComposeNets(nets[4], nets[5]);
            ComposeNets(nets[6], nets[7]);
            ComposeNets(nets[9], nets[10], nets[11], nets[12]);
            ComposeNets(nets[13], nets[14], nets[15], nets[16]);
            ComposeNets(nets[8], nets[17]);

            Console.Read();
        }

        internal static class Operations
        {
            internal static string FixBinaryView(string binaryOctet, int wantedLength)
            {
                int res = Math.Abs(binaryOctet.Length - wantedLength);
                return $"{ string.Empty.PadRight(res, '0')}{binaryOctet}";
            }

            internal static void ComposeNets(params Net[] nets) // from 3 to 4 nets
            {
                //var subnetOctet3Part1 = nets[0].subnetBinaryOctet3Part1;
                var subnetOctet3Part1 = FixBinaryView(Convert.ToString(composeSubnetCounter, 2), 5);
                //if (nets[0].subNetOctet3Part2[0] != '0' | nets[0].subNetOctet3Part2[0] != '0') throw new Exception("RUKiddin'Me?");
                var subSubnetBinaryString = string.Empty;

                Console.WriteLine($"\n{string.Empty.PadRight(35, '+')} Compose Nets Start {string.Empty.PadRight(35, '+')}");
                for (int subSubnetCounter = 0; subSubnetCounter < nets.Length; subSubnetCounter++)
                {
                    if (nets.Length != 2) subSubnetBinaryString = FixBinaryView(Convert.ToString(subSubnetCounter, 2), 2);
                    else subSubnetBinaryString = Convert.ToString(subSubnetCounter, 2);

                     var binFullHostNewNet = FixBinaryView(Convert.ToString(nets[subSubnetCounter].numOfHosts, 2), 11);
                    if (nets.Length != 2) binFullHostNewNet = $"{subSubnetBinaryString}{binFullHostNewNet.Substring(2)}";
                    else
                    {
                        binFullHostNewNet = $"{subSubnetBinaryString}{binFullHostNewNet.Substring(1)}";
                        subSubnetBinaryString = subSubnetBinaryString + "0";
                    }
                    string subNetOctet3Part2 = binFullHostNewNet.Substring(0, 3);
                    int fullOctet3Decimal = Convert.ToInt32($"{subnetOctet3Part1}{subNetOctet3Part2}", 2);
                    string hostOctet4 = binFullHostNewNet.Substring(3);
                    int hostOctet4Decimal = Convert.ToInt32(hostOctet4, 2);
                    var clearDefaultSubnetByOctetDecimal = Convert.ToInt32(($"{subnetOctet3Part1}{subSubnetBinaryString}0"), 2);

                    Console.WriteLine(
$@"Number of subnets: {nets[subSubnetCounter].numOfHosts}
                    Lower border net{subSubnetCounter}:   {beginning}.{clearDefaultSubnetByOctetDecimal}.1  | {beginning}.{FixBinaryView(subnetOctet3Part1, 5)}_{subSubnetBinaryString}0.{FixBinaryView("1", 8)}
                      Top border net{subSubnetCounter}:   {beginning}.{fullOctet3Decimal}.{hostOctet4Decimal} | {beginning}.{FixBinaryView(subnetOctet3Part1, 5)}_{subNetOctet3Part2}.{FixBinaryView(hostOctet4, 8)}");
                }
                Console.WriteLine($"\n{string.Empty.PadRight(35, '+')} Compose Nets End {string.Empty.PadRight(35, '+')}\n");
                composeSubnetCounter++;
            }
        }

        internal class Net
        {
            public string subnetBinaryOctet3Part1 { get; set; }
            public string subNetOctet3Part2 { get; set; }
            public string hostOctet4 { get; set; }
            public int hostOctet4Decimal { get; set; }
            public int subNetOctet3DecimalFull { get; set; }
            public int numOfHosts { get; set; }
        }
    }
}

//            internal static void ComposeNets(Net net1, Net net2, string a)
//            {
//                var subnetBinaryOctet3Part1 = FixBinaryView(Convert.ToString(composeSubnetCounter, 2), 5);
//                if (net1.subNetOctet3Part2[0] != '0') throw new Exception("RUKiddin'Me?");
//                //var subnetBinaryOctet3Part1 = net1.subnetBinaryOctet3Part1;
//                var binFullHostNewNet = FixBinaryView(Convert.ToString(net2.numOfHosts, 2), 11);
//                binFullHostNewNet = $"{1}{binFullHostNewNet.Substring(1)}";
//                var subNetOctet3Part2 = binFullHostNewNet.Substring(0, 3);
//                var hostOctet4 = binFullHostNewNet.Substring(3);
//                var hostOctet4Decimal = Convert.ToInt32(hostOctet4, 2);
//                var subNetOctet3DecimalFull = Convert.ToInt32((subnetBinaryOctet3Part1 + subNetOctet3Part2), 2);
//                var clearDefaultSubnetByOctetDecimalNet1 = Convert.ToInt32((subnetBinaryOctet3Part1 + "000"), 2);
//                var clearDefaultSubnetByOctetDecimalNet2 = Convert.ToInt32((subnetBinaryOctet3Part1 + "100"), 2);

//                Console.WriteLine($"\n{string.Empty.PadRight(35, '+')} Compose Nets Start {string.Empty.PadRight(35, '+')}");
//                Console.WriteLine(
//$@"Number of subnets: {net1.numOfHosts}
//                    Lower border net1:      {beginning}.{clearDefaultSubnetByOctetDecimalNet1}.1  | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_000.{FixBinaryView("1", 8)}
//                    Top border net1:        {beginning}.{subNetOctet3DecimalFull}.{net1.hostOctet4Decimal} | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_{net1.subNetOctet3Part2}.{FixBinaryView(net1.hostOctet4, 8)}
//Number of subnets: {net2.numOfHosts}
//                    Lower border net2:      {beginning}.{clearDefaultSubnetByOctetDecimalNet2}.1  | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_100.{FixBinaryView("1", 8)}
//                    Top border net2:        {beginning}.{subNetOctet3DecimalFull}.{hostOctet4Decimal} | {beginning}.{FixBinaryView(subnetBinaryOctet3Part1, 5)}_{subNetOctet3Part2}.{FixBinaryView(hostOctet4, 8)}");
//                Console.WriteLine($"\n{string.Empty.PadRight(35, '+')} Compose Nets End {string.Empty.PadRight(35, '+')}\n");
//                composeSubnetCounter++;
//            }

