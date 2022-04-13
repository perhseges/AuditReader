using System.IO.Compression;
using System.Text.Json;

public static class GzipBytes
{
    /// <summary>
    /// Gzip from a stream to another stream
    /// </summary>
    /// <param name="inStream"></param>
    /// <param name="outStream"></param>
    /// <param name="gzipMode"></param>
    public static void GZipStream(this Stream inStream, Stream outStream, CompressionMode gzipMode = CompressionMode.Compress)
    {
        if (gzipMode == CompressionMode.Compress)
        {
            using (var compressionStream = new GZipStream(outStream, CompressionMode.Compress))
            {
                inStream.CopyTo(compressionStream);
            }
        }
        else
        {
            using (var compressionStream = new GZipStream(inStream, CompressionMode.Decompress))
            {
                compressionStream.CopyTo(outStream);
            }
        }
    }

    /// <summary>
    /// Gzip a byte array to a new gzipped byte array
    /// /// </summary>
    /// <param name="inBytes"></param>
    /// <param name="gzipMode"></param>
    /// <returns>Byte array</returns>

    public static byte[] GZipBytes(this byte[] inBytes, CompressionMode gzipMode = CompressionMode.Compress)
    {
        using (var msi = new MemoryStream(inBytes))
        {
            using (var mso = new MemoryStream())
            {
                msi.GZipStream(mso, gzipMode);
                return mso.ToArray();
            }
        }
    }

    /// <summary>
    /// Gzip a stream to a new gzipped byte array
    /// /// </summary>
    /// <param name="inBytes"></param>
    /// <param name="gzipMode"></param>
    /// <returns>Byte array</returns>

    public static byte[] GZipBytes(this Stream inStream, CompressionMode gzipMode = CompressionMode.Compress)
    {
        inStream.Position = 0;
        using (var mso = new MemoryStream())
        {
            inStream.GZipStream(mso, gzipMode);
            return mso.ToArray();
        }
    }

    public static async Task<byte[]> FromObjectAsync<T>(T data)
    {
        using var datastream = new MemoryStream();
        await JsonSerializer.SerializeAsync(datastream, data);
        var compressedbytes = datastream.GZipBytes(CompressionMode.Compress);
        return compressedbytes;
    }

    public static async Task<T?> ToObjectAsync<T>(byte[] compressedbytes)
    {
        var decompressedbytes = compressedbytes.GZipBytes(CompressionMode.Decompress);

        using var datastream = new MemoryStream(decompressedbytes);
        T? decompressedobject = await JsonSerializer.DeserializeAsync<T>(datastream);

        return decompressedobject;
    }

}
