using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web;
using Videothek.Core;
using System.IO;
using System.Net.Http;

namespace Videothek.Formatter
{
    public class CsvFormatter : BufferedMediaTypeFormatter
    {
        public CsvFormatter()
        {
            //Best Practise in Mime-Type
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }


        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == typeof(Movie))
                return true;

            Type movieList = typeof(List<Movie>);
            return movieList.IsAssignableFrom(type);
        }


        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            List<Movie> movies = value as List<Movie>;

            using (StreamWriter writer = new StreamWriter(writeStream))
            {
                if (movies != null)
                {
                    //Stream
                    foreach (var movie in movies)
                    {
                        writer.WriteLine($"{movie.Id},{movie.Name},{movie.GenreId},{movie.Genre.Name}");
                    }
                }
                else
                {
                    Movie movie = (Movie)value;

                    //Stream
                    writer.WriteLine($"{movie.Id},{movie.Name},{movie.GenreId},{movie.Genre.Name}");
                }
            }
        }
    }
}