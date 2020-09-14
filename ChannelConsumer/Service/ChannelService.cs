using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelConsumer.Service
{
   public class ChannelService
   {
       private Channel<string> serviceChannel;

       public ChannelService()
       {
           serviceChannel = Channel.CreateBounded<string>(new BoundedChannelOptions(30)
           {
               SingleReader = false,
               SingleWriter = false
           });
       }


       public async Task AddToChannelAsync(string content,CancellationToken token)
       {
           
           await serviceChannel.Writer.WriteAsync(content, token);

       }

       public IAsyncEnumerable<string> ReturnValue(CancellationToken token) =>
           serviceChannel.Reader.ReadAllAsync(token);
   }
}
