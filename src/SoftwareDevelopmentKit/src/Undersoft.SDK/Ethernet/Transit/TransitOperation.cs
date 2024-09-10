using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.Json;

namespace Undersoft.SDK.Ethernet
{
    public class TransitOperation
    {
        private DirectionType direction;
        private ProtocolMethod method;
        private TransitPart part;
        private EthernetProtocol protocol;
        private EthernetSite site;
        private EthernetTransit transit;
        private ITransitContext transitContext;
        private EthernetContext ethernetContext;

        public TransitOperation(
            EthernetTransit _transaction,
            TransitPart _part,
            DirectionType _direction
        )
        {
            transit = _transaction;
            transitContext = transit.Context;
            ethernetContext = transit.MyHeader.Context;
            site = ethernetContext.IdentitySite;
            direction = _direction;
            part = _part;
            protocol = transitContext.Protocol;
            method = transitContext.Method;
        }

        public void Resolve(ITransitBuffer buffer = null)
        {
            switch (site)
            {
                case EthernetSite.Server:
                    switch (direction)
                    {
                        case DirectionType.Receive:
                            switch (part)
                            {
                                case TransitPart.Header:
                                    ServerReceivedTcpTransitHeader(buffer);
                                    break;
                                case TransitPart.Message:
                                    ServerReceivedTcpTransitMessage(buffer);
                                    break;
                            }
                            break;
                        case DirectionType.Send:
                            switch (part)
                            {
                                case TransitPart.Header:
                                    ServerSendTcpTransitHeader();
                                    break;
                                case TransitPart.Message:
                                    ServerSendTcpTransitMessage();
                                    break;
                            }
                            break;
                    }
                    break;
                case EthernetSite.Client:
                    switch (direction)
                    {
                        case DirectionType.Receive:
                            switch (part)
                            {
                                case TransitPart.Header:
                                    ClientReceivedTcpTransitHeader(buffer);
                                    break;
                                case TransitPart.Message:
                                    ClientReceivedTcpTransitMessage(buffer);
                                    break;
                            }
                            break;
                        case DirectionType.Send:
                            switch (part)
                            {
                                case TransitPart.Header:
                                    ClientSendTcpTransitHeader();
                                    break;
                                case TransitPart.Message:
                                    ClientSendTcpTrnsitMessage();
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void ClientReceivedTcpTransitHeader(ITransitBuffer buffer)
        {
            TransitHeader headerObject = (TransitHeader)transit.MyHeader.Deserialize(buffer);

            if (headerObject != null)
            {
                transit.HeaderReceived = headerObject;               

                object reciveContent = transit.HeaderReceived.Data;

                Type[] ifaces = reciveContent.GetType().GetInterfaces();
                if (
                    ifaces.Contains(typeof(ITransitable))
                    && ifaces.Contains(typeof(ITransitObject))
                )
                {
                    if (transit.MyHeader.Data == null)
                        transit.MyHeader.Data = ((ITransitObject)reciveContent).Locate();

                    object myContent = transit.MyHeader.Data;

                    ((ITransitObject)myContent).Merge(reciveContent);

                    int objectCount = transit.HeaderReceived.Context.ItemsCount;
                    if (objectCount == 0)
                        transitContext.HasMessageToReceive = false;
                    else
                        transit.MessageReceived = new TransitMessage(
                            transit,
                            DirectionType.Receive,
                            myContent
                        );
                }
                else if (reciveContent is Hashtable)
                {
                    Hashtable hashTable = (Hashtable)reciveContent;
                    if (hashTable.Contains("Register"))
                    {
                        transitContext.Denied = !(bool)hashTable["Register"];
                        if (transitContext.Denied)
                        {
                            transitContext.Close = true;
                            transitContext.HasMessageToReceive = false;
                            transitContext.HasMessageToSend = false;
                        }
                    }
                }
                else
                    transitContext.HasMessageToSend = false;
            }
        }

        private void ClientReceivedTcpTransitMessage(ITransitBuffer buffer)
        {
            object serialItemsObj = ((object[])transit.MessageReceived.Data)[
                buffer.InputId
            ];
            ITransitable serialItems = (ITransitable)serialItemsObj;

            object deserialItemsObj = serialItems.Deserialize(buffer);
            ITransitable deserialItems = (ITransitable)deserialItemsObj;
            if (
                deserialItems.InputChunks <= deserialItems.CurrentChunk
                || deserialItems.CurrentChunk == 0
            )
            {
                transit.Context.ItemsLeft--;
                deserialItems.CurrentChunk = 0;
            }
        }

        private void ClientSendTcpTransitHeader()
        {
            transit.Manager.HeaderContent(
                transitContext.Transfer.MyHeader.Data,
                transitContext.Transfer.MyHeader.Data,
                DirectionType.Send
            );

            if (transit.MyHeader.Context.ItemsCount == 0)
                transitContext.HasMessageToSend = false;

            transitContext.Transfer.MyHeader.Serialize(transitContext, 0, 0);
        }

        private void ClientSendTcpTrnsitMessage()
        {
            object serialitems = ((object[])transit.MyMessage.Data)[
                transitContext.ItemIndex
            ];

            int serialBlockId = ((ITransitable)serialitems).Serialize(
                transitContext,
                transitContext.OutputId,
                5000
            );
            if (serialBlockId < 0)
            {
                if (
                    transitContext.ItemIndex < (transit.MyHeader.Context.ItemsCount - 1)
                )
                {
                    transitContext.ItemIndex++;
                    transitContext.OutputId = 0;
                    return;
                }
            }
            transitContext.OutputId = serialBlockId;
        }
      
        private void ServerReceivedTcpTransitHeader(ITransitBuffer buffer)
        {
            bool isError = false;
            string errorMessage = "";
            try
            {
                TransitHeader headerObject = (TransitHeader)transit.MyHeader.Deserialize(buffer);
                if (headerObject != null)
                {
                    transit.HeaderReceived = headerObject;


                        if (transit.HeaderReceived.Context.ContentType != null)
                        {
                            object _content = transit.HeaderReceived.Data;

                            Type[] ifaces = _content.GetType().GetInterfaces();
                            if (
                                ifaces.Contains(typeof(ITransitable))
                                && ifaces.Contains(typeof(ITransitObject))
                            )
                            {
                                int objectCount = transit.HeaderReceived.Context.ItemsCount;
                                transitContext.Synchronic = transit
                                    .HeaderReceived
                                    .Context
                                    .Synchronic;

                                object myheader = ((ITransitObject)_content).Locate();

                                if (myheader != null)
                                {
                                    if (objectCount == 0)
                                    {
                                        transitContext.HasMessageToReceive = false;

                                        transit.MyHeader.Data = myheader;
                                    }
                                    else
                                    {
                                        transit.MyHeader.Data = (
                                            (ITransitObject)myheader
                                        ).Merge(_content);
                                        transit.MessageReceived = new TransitMessage(
                                            transit,
                                            DirectionType.Receive,
                                            transit.MyHeader.Data
                                        );
                                    }
                                }
                                else
                                {
                                    isError = true;
                                    errorMessage += "Prime not exist - incorrect object target ";
                                }
                            }
                            else
                            {
                                isError = true;
                                errorMessage += "Incorrect DPOT object - deserialization error ";
                            }
                        }
                        else
                        {
                            transit.MyHeader.Data = new Hashtable() { { "Register", true } };
                            transit.MyHeader.Context.Echo +=
                                "Registration success - ContentType: null ";
                        }                    
                }
                else
                {
                    isError = true;
                    errorMessage += "Incorrect DPOT object - deserialization error ";
                }
            }
            catch (Exception ex)
            {
                isError = true;
                errorMessage += ex.ToString();
            }

            if (isError)
            {
                transit.Context.Close = true;
                transit.Context.HasMessageToReceive = false;
                transit.Context.HasMessageToSend = false;

                if (errorMessage != "")
                {
                    transit.MyHeader.Data += errorMessage;
                    transit.MyHeader.Context.Echo += errorMessage;
                }
                transit.MyHeader.Context.Errors++;
            }
        }

        private void ServerReceivedTcpTransitMessage(ITransitBuffer buffer)
        {
            object serialItemsObj = ((object[])transit.MessageReceived.Data)[
                buffer.InputId
            ];
            object deserialItemsObj = ((ITransitable)serialItemsObj).Deserialize(buffer);
            ITransitable deserialItems = (ITransitable)deserialItemsObj;
            if (
                deserialItems.InputChunks <= deserialItems.CurrentChunk
                || deserialItems.CurrentChunk == 0
            )
            {
                transit.Context.ItemsLeft--;
                deserialItems.CurrentChunk = 0;
            }
        }

        private void ServerSendTcpTransitHeader()
        {
            transit.Manager.HeaderContent(
                transitContext.Transfer.MyHeader.Data,
                transitContext.Transfer.MyHeader.Data,
                DirectionType.Send
            );

            if (transit.MyHeader.Context.ItemsCount == 0)
                transitContext.HasMessageToSend = false;

            transitContext.Transfer.MyHeader.Serialize(transitContext, 0, 0);
        }

        private void ServerSendTcpTransitMessage()
        {
            int serialBlockId = ((ITransitable[])transit.MyMessage.Data)[
                transitContext.ItemIndex
            ].Serialize(transitContext, transitContext.OutputId, 5000);

            if (serialBlockId < 0)
            {
                if (
                    transitContext.ItemIndex < (transit.MyHeader.Context.ItemsCount - 1)
                )
                {
                    transitContext.ItemIndex++;
                    transitContext.OutputId = 0;
                    return;
                }
            }
            transitContext.OutputId = serialBlockId;
        }    
    }
}
