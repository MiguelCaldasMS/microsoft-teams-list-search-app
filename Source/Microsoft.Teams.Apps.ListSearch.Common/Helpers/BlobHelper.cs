﻿// <copyright file="BlobHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.ListSearch.Common.Helpers
{
    using System.Threading.Tasks;
    using Microsoft.Teams.Apps.ListSearch.Common.Models;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// Helper for blob.
    /// </summary>
    public class BlobHelper
    {
        private static readonly string BlobContainerName = StorageInfo.BlobContainerName;
        private readonly CloudStorageAccount storageAccount;
        private readonly CloudBlobClient cloudBlobClient;
        private readonly CloudBlobContainer cloudBlobContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobHelper"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string of storage.</param>
        public BlobHelper(string connectionString)
        {
            this.storageAccount = CloudStorageAccount.Parse(connectionString);
            this.cloudBlobClient = this.storageAccount.CreateCloudBlobClient();
            this.cloudBlobContainer = this.cloudBlobClient.GetContainerReference(BlobContainerName);
        }

        /// <summary>
        /// Upload blob
        /// </summary>
        /// <param name="fileContents">Contents of file to be uploaded.</param>
        /// <param name="blobName">Name of the blob</param>
        /// <returns><see cref="Task"/> That represents upload operation.</returns>
        public async Task<string> UploadBlobAsync(string fileContents, string blobName)
        {
            CloudBlockBlob cloudBlockBlob = this.cloudBlobContainer.GetBlockBlobReference(blobName);
            await cloudBlockBlob.UploadTextAsync(fileContents);

            return cloudBlockBlob.Uri.ToString();
        }

        /// <summary>
        /// Delete blob.
        /// </summary>
        /// <param name="blobName">Name of the blob to be deleted.</param>
        /// <returns><see cref="Task"/> That represents the delete operation.</returns>
        public async Task DeleteBlobAsync(string blobName)
        {
            CloudBlockBlob cloudBlockBlob = this.cloudBlobContainer.GetBlockBlobReference(blobName);
            await cloudBlockBlob.DeleteIfExistsAsync();
        }
    }
}
