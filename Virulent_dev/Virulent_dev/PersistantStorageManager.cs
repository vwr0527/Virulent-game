using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework;
using System.IO;

namespace Virulent_dev
{
    class PersistantStorageManager
    {
        StorageDevice device;
        IAsyncResult result;
        bool gameSaveRequested = false;

        public PersistantStorageManager(StorageDevice deviceParam)
        {
            device = deviceParam;
        }


        public void DoSaveRequest(bool guideIsVisible, PlayerIndex whichPlayer)
        {
            // Set the request flag
            if ((!guideIsVisible) && (gameSaveRequested == false))
            {
                gameSaveRequested = true;
                result = StorageDevice.BeginShowSelector(
                        whichPlayer, null, null);
            }
        }

        public void DoPendingSave()
        {
            if ((gameSaveRequested) && (result.IsCompleted))
            {
                device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    //this is where the saving actually happens.
                    MessWithFiles();
                }
                // Reset the request flag
                gameSaveRequested = false;
            }
        }

        private void MessWithFiles()
        {

            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            // Add the container path to our file name.
            string filename = "demobinary.sav";

            // Create a new file.
            if (!container.FileExists(filename))
            {
                Stream file = container.CreateFile(filename);
                file.Close();
            }
            // Dispose the container, to commit the data.
            container.Dispose();
        }
    }
}
