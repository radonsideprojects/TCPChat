using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace Client.Classes
{
    public class Notifications
    {
        private static List<WaveOutEvent> players = new List<WaveOutEvent>(); // Keeps track of all the players

        public static void PlayIncoming()
        {
            PlaySound("Incoming.wav");
        }

        public static void PlayIncomingAlt()
        {
            PlaySound("IncomingAlt.wav");
        }

        public static void PlayJoin()
        {
            PlaySound("Join.wav");
        }

        public static void PlayLeft()
        {
            PlaySound("Leave.wav");
        }

        private static void PlaySound(string soundFileName)
        {
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", soundFileName);

            if (File.Exists(soundPath))
            {
                var player = new WaveOutEvent();
                var reader = new WaveFileReader(soundPath);

                player.Init(reader);
                player.Play();

                players.Add(player);

                player.PlaybackStopped += (sender, args) =>
                {
                    player.Dispose();
                    players.Remove(player);
                };
            }
        }
    }
}
