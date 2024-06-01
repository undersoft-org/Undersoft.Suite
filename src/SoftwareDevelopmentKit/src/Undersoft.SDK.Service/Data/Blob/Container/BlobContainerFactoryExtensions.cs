﻿using Undersoft.SDK.Service.Data.Blob;

namespace Undersoft.SDK.Service.Data.Blob.Container
{
    public static class BlobContainerFactoryExtensions
    {
        /// <summary>
        /// Gets a named container.
        /// </summary>
        /// <param name="blobContainerFactory">The blob container manager</param>
        /// <returns>
        /// The container object.
        /// </returns>
        public static IBlobContainer Create<TContainer>(
            this IBlobContainerFactory blobContainerFactory
        )
        {
            return blobContainerFactory.Create(
                BlobContainerNameAttribute.GetContainerName<TContainer>()
            );
        }
    }
}
