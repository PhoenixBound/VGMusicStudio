﻿namespace Kermalis.VGMusicStudio.Core.GBA.AlphaDream
{
    internal class Track
    {
        public readonly byte Index;
        public readonly string Type;
        public readonly Channel Channel;

        public byte Voice;
        public byte PitchBendRange;
        public byte Volume;
        public byte Rest;
        public byte NoteDuration;
        public sbyte PitchBend;
        public sbyte Panpot;
        public bool Enabled;
        public bool Stopped;
        public int CurEvent;
        public ICommand PrevCommand;

        public int GetPitch()
        {
            return PitchBend * (PitchBendRange / 2);
        }

        public Track(byte i, Mixer mixer)
        {
            Index = i;
            // TODO: PSG Channels 3 and 4 are also usable
            Type = i >= 8 ? i % 2 == 0 ? "Square 1" : "Square 2" : "PCM8";
            Channel = i >= 8 ? (Channel)new SquareChannel(mixer) : new PCMChannel(mixer);
        }
        public void Init()
        {
            Voice = 0;
            Rest = 0;
            PitchBendRange = 2;
            NoteDuration = 0;
            PitchBend = 0;
            Panpot = 0;
            CurEvent = 0;
            Stopped = false;
            Volume = 0x7F;
            PrevCommand = null;
        }
        public void Tick()
        {
            if (Rest != 0)
            {
                Rest--;
            }
            if (NoteDuration > 0)
            {
                NoteDuration--;
            }
        }
    }
}
