using System;

namespace LoopLib
{
    class Wave
    {
        public int FrameNumber;
        public Universe Universe;

        public void Advance(int delta)
        {
            if (FrameNumber + delta >= Universe.Frames.Length)
            {
                delta = Universe.Frames.Length - FrameNumber - 1;
            }

            for (int i = FrameNumber + 1; i <= FrameNumber + delta; i++)
            {
                Universe.Frames[i].InitFrom(Universe.Frames[i - 1]);
            }
            FrameNumber += delta;
        }

        public void AddClientEvent(ClientEvent e)
        {
            var eventFrame = (int)Math.Floor(e.GameTime * Universe.TickRate);
            if (eventFrame > FrameNumber)
                throw new ArgumentException("Event happening in the future.");

            Universe.Frames[eventFrame].ClientEvents[e.DrivingEntityId] = e;
            var oldFrame = FrameNumber;
            FrameNumber = eventFrame - 1;
            // Fast forward from event time to wave now
            Advance(oldFrame - eventFrame);

        }

        public void SetGameTime(float gameTime)
        {
            var newNumber = ((int)Math.Floor(gameTime * Universe.TickRate)) + 1;
            if(newNumber < FrameNumber)
                throw new ArgumentException("Unable to rewind time.");

            int delta = newNumber - FrameNumber;
            if(delta > 0)
                Advance(delta);

        }
    }
}
