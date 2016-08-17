using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    /// <summary>
    /// A generic socket exception class that holds an error, and
    /// an optional text string describing the error in more detail.
    /// </summary>
    class SocketLibException : Exception
    {
        protected ErrorCode _code;

        public SocketLibException(System.Net.Sockets.SocketError code)
        {
            _code = TranslateError(code);
        }

        public SocketLibException(ErrorCode code)
        {
            _code = code;
        }

        public static ErrorCode TranslateError(System.Net.Sockets.SocketError sErr)
        {
            switch (sErr)
            {
                case System.Net.Sockets.SocketError.Interrupted:
                    return ErrorCode.EInterrupted;
                case System.Net.Sockets.SocketError.AccessDenied:
                    return ErrorCode.EAccessDenied;
                case System.Net.Sockets.SocketError.InvalidArgument:
                    return ErrorCode.EInvalidParameter;
                case System.Net.Sockets.SocketError.TooManyOpenSockets:
                    return ErrorCode.ENoSocketsAvailable;
                case System.Net.Sockets.SocketError.WouldBlock:
                    return ErrorCode.EOperationWouldBlock;
                case System.Net.Sockets.SocketError.InProgress:
                case System.Net.Sockets.SocketError.AlreadyInProgress:
                    return ErrorCode.EInProgress;
                case System.Net.Sockets.SocketError.NotSocket:
                    return ErrorCode.EInvalidSocket;
                case System.Net.Sockets.SocketError.DestinationAddressRequired:
                    return ErrorCode.EAddressRequired;
                case System.Net.Sockets.SocketError.MessageSize:
                    return ErrorCode.EMessageTooLong;
                case System.Net.Sockets.SocketError.ProtocolType:
                    return ErrorCode.EProtocolNotSupportedBySocket;
                case System.Net.Sockets.SocketError.ProtocolOption:
                    return ErrorCode.EBadProtocolOption;
                case System.Net.Sockets.SocketError.ProtocolNotSupported:
                    return ErrorCode.EProtocolNotSupported;
                case System.Net.Sockets.SocketError.SocketNotSupported:
                    return ErrorCode.EInvalidSocketType;
                case System.Net.Sockets.SocketError.OperationNotSupported:
                    return ErrorCode.EOperationNotSupported;
                case System.Net.Sockets.SocketError.ProtocolFamilyNotSupported:
                    return ErrorCode.EProtocolFamilyNotSupported;
                case System.Net.Sockets.SocketError.AddressFamilyNotSupported:
                    return ErrorCode.EAddressFamilyNotSupported;
                case System.Net.Sockets.SocketError.AddressAlreadyInUse:
                    return ErrorCode.EAddressInUse;
                case System.Net.Sockets.SocketError.AddressNotAvailable:
                    return ErrorCode.EAddressNotAvailable;
                case System.Net.Sockets.SocketError.NetworkDown:
                    return ErrorCode.ENetworkDown;
                case System.Net.Sockets.SocketError.NetworkUnreachable:
                    return ErrorCode.ENetworkUnreachable;
                case System.Net.Sockets.SocketError.NetworkReset:
                    return ErrorCode.ENetworkReset;
                case System.Net.Sockets.SocketError.ConnectionAborted:
                    return ErrorCode.EConnectionAborted;
                case System.Net.Sockets.SocketError.ConnectionReset:
                    return ErrorCode.EConnectionReset;
                case System.Net.Sockets.SocketError.NoBufferSpaceAvailable:
                    return ErrorCode.ENoMemory;
                case System.Net.Sockets.SocketError.IsConnected:
                    return ErrorCode.EAlreadyConnected;
                case System.Net.Sockets.SocketError.NotConnected:
                    return ErrorCode.ENotConnected;
                case System.Net.Sockets.SocketError.Shutdown:
                    return ErrorCode.EShutDown;
                case System.Net.Sockets.SocketError.TimedOut:
                    return ErrorCode.ETimedOut;
                case System.Net.Sockets.SocketError.ConnectionRefused:
                    return ErrorCode.EConnectionRefused;
                case System.Net.Sockets.SocketError.HostDown:
                    return ErrorCode.EHostDown;
                case System.Net.Sockets.SocketError.HostUnreachable:
                    return ErrorCode.EHostUnreachable;
                case System.Net.Sockets.SocketError.HostNotFound:
                    return ErrorCode.EDNSNotFound;
                case System.Net.Sockets.SocketError.TryAgain:
                    return ErrorCode.EDNSError;
                case System.Net.Sockets.SocketError.NoData:
                    return ErrorCode.ENoDNSData;
                default:
                    return ErrorCode.ESeriousError;
            }
        }

        // ====================================================================
        // Function:    Error
        // Purpose:     To retrieve the error code of the socket.
        // ====================================================================
        public ErrorCode ErrorCode
        {
            get
            {
                return _code;
            }
        }

        // ====================================================================
        // Function:    PrintError
        // Purpose:     Print the error message to a string
        // ====================================================================
        public string ErrorMessage
        {
            get
            {
                switch (_code)
                {
                    case ErrorCode.EOperationWouldBlock:
                        return "Nonblocking socket operation would have blocked";
                    case ErrorCode.EInProgress:
                        return "This operation is already in progress";
                    case ErrorCode.EInvalidSocket:
                        return "The socket was not valid";
                    case ErrorCode.EAddressRequired:
                        return "A destination address is required";
                    case ErrorCode.EMessageTooLong:
                        return "The message was too long";
                    case ErrorCode.EProtocolNotSupported:
                        return "The protocol is not supported";
                    case ErrorCode.EProtocolNotSupportedBySocket:
                        return "The socket type is not supported";
                    case ErrorCode.EOperationNotSupported:
                        return "The operation is not supported";
                    case ErrorCode.EProtocolFamilyNotSupported:
                        return "The protocol family is not supported";
                    case ErrorCode.EAddressFamilyNotSupported:
                        return "The operation is not supported by the address family";
                    case ErrorCode.EAddressInUse:
                        return "The address is already in use";
                    case ErrorCode.EAddressNotAvailable:
                        return "The address is not available to use";
                    case ErrorCode.ENetworkDown:
                        return "The network is down";
                    case ErrorCode.ENetworkUnreachable:
                        return "The destination network is unreachable";
                    case ErrorCode.ENetworkReset:
                        return "The network connection has been reset";
                    case ErrorCode.EConnectionAborted:
                        return "The network connection has been aborted due to software error";
                    case ErrorCode.EConnectionReset:
                        return "Connection has been closed by the other side";
                    case ErrorCode.ENoMemory:
                        return "There was insufficient system memory to complete the operation";
                    case ErrorCode.EAlreadyConnected:
                        return "The socket is already connected";
                    case ErrorCode.ENotConnected:
                        return "The socket is not connected";
                    case ErrorCode.EShutDown:
                        return "The socket has already been shut down";
                    case ErrorCode.ETimedOut:
                        return "The connection timed out";
                    case ErrorCode.EConnectionRefused:
                        return "The connection was refused";
                    case ErrorCode.EHostDown:
                        return "The host is down";
                    case ErrorCode.EHostUnreachable:
                        return "The host is unreachable";
                    case ErrorCode.EDNSNotFound:
                        return "DNS lookup is not found";
                    case ErrorCode.EDNSError:
                        return "Host not found due to error; try again";
                    case ErrorCode.ENoDNSData:
                        return "Address found, but has no valid data";
                    case ErrorCode.EInterrupted:
                        return "Operation was interrupted";
                    case ErrorCode.ENoSocketsAvailable:
                        return "No more sockets available";
                    case ErrorCode.EInvalidParameter:
                        return "Operation has an invalid parameter";
                    case ErrorCode.EInvalidSocketType:
                        return "Socket type is invalid";
                    case ErrorCode.EAccessDenied:
                        return "Access to this operation was denied";
                    case ErrorCode.ESocketLimitReached:
                        return "The manager has reached its maximum number of sockets";
                    default:
                        return "undefined or serious error";
                }
            }
        }
    }
}
