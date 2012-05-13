using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SagaMap.Tasks.SystemTasks
{
    class GameTime
    {
        private ReaderWriterLock gameTimeLock = new ReaderWriterLock();
        private byte[] gameTime;
        private Timer gameTimeTimer;

        public GameTime()
        {
            // 1min in the game are 5 "normal" seconds
            this.gameTime = new byte[3];
            this.gameTime[0] = 1; // day 1
            this.gameTime[1] = 0; // hour 0
            this.gameTime[2] = 0; // min 0
            this.gameTimeTimer = new Timer(new TimerCallback(this.gameTimeCallback), null, 10, 5000);
        }
 
        private void gameTimeCallback(Object state)
        {
            try
            {
                this.gameTimeLock.AcquireWriterLock(Timeout.Infinite);

                if (this.gameTime[2] < 59) this.gameTime[2]++;
                else
                {
                    if (this.gameTime[1] < 24)
                    {
                        this.gameTime[1]++;
                        this.gameTime[2] = 0;
                    }
                    else
                    {
                        if (this.gameTime[0] < 28)
                        {
                            this.gameTime[0]++;
                            this.gameTime[1] = 0;
                            this.gameTime[2] = 0;
                        }
                        else
                        {
                            this.gameTime[0] = 1;
                            this.gameTime[1] = 0;
                            this.gameTime[2] = 0;
                        }
                    }
                }
            }
            finally
            {
                this.gameTimeLock.ReleaseWriterLock();
            }
        }

        public byte[] GetTime()
        {
            byte[] ret = new byte[3];
            try
            {
                this.gameTimeLock.AcquireReaderLock(Timeout.Infinite);
                this.gameTime.CopyTo(ret, 0);
            }
            finally
            {
                this.gameTimeLock.ReleaseReaderLock();
            }

            return ret;
        }

        public void UpdateTime(byte day, byte hour, byte min)
        {
            try
            {
                this.gameTimeLock.AcquireWriterLock(Timeout.Infinite);
                this.gameTime[0] = day;
                this.gameTime[1] = hour;
                this.gameTime[2] = min;
            }
            finally
            {
                this.gameTimeLock.ReleaseWriterLock();
            }
        }

    }
}
