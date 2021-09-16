using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
    // TODO: place structs and delegate definitions here

    public partial class SDK
    {
        public partial class XBL
        {
            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionDuplicateHandle API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionduplicatehandle
            /// </summary>
            /// <param name="srcHandle"></param>
            /// <param name="dstHandle"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionDuplicateHandle(
                XblMultiplayerSessionHandle srcHandle,
                out XblMultiplayerSessionHandle dstHandle)
            {
                var interopDstHandle = new Interop.XblMultiplayerSessionHandle();
                int result;

                unsafe
                {
                    result = Multiplayer.XblMultiplayerSessionDuplicateHandle(
                        srcHandle.InteropHandle.handle,
                        &interopDstHandle.handle);

                    if (HR.SUCCEEDED(result))
                    {
                        dstHandle = new XblMultiplayerSessionHandle(interopDstHandle);
                    }
                    else
                    {
                        dstHandle = null;
                    }
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionTimeOfSession API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessiontimeofsession
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <returns>a DateTime representing when the session was created.</returns>
            public static DateTime XblMultiplayerSessionTimeOfSession(
                XblMultiplayerSessionHandle sessionHandle)
            {
                var result = Multiplayer.XblMultiplayerSessionTimeOfSession(
                    sessionHandle.InteropHandle.handle);
                var time = new TimeT(result);
                return time.DateTime;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionGetInitializationInfo API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessiongetinitializationinfo
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <returns>null if info does not exist for the handle, non-null otherwise.</returns>
            public static XblMultiplayerSessionInitializationInfo XblMultiplayerSessionGetInitializationInfo(
                XblMultiplayerSessionHandle sessionHandle)
            {
                XblMultiplayerSessionInitializationInfo info = null;

                unsafe
                {
                    var result = Multiplayer.XblMultiplayerSessionGetInitializationInfo(
                        sessionHandle.InteropHandle.handle);
                    if (result != default(Interop.XblMultiplayerSessionInitializationInfo*))
                    {
                        info = new XblMultiplayerSessionInitializationInfo(*result);
                    }
                }

                return info;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionSubscribedChangeTypes API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionsubscribedchangetypes
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <returns>Combined bit flags that contain supported change types by the session.</returns>
            public static XblMultiplayerSessionChangeTypes XblMultiplayerSessionSubscribedChangeTypes(
                XblMultiplayerSessionHandle sessionHandle)
            {
                return Multiplayer.XblMultiplayerSessionSubscribedChangeTypes(
                    sessionHandle.InteropHandle.handle);
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionHostCandidates API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionhostcandidates
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="deviceTokens"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionHostCandidates(
                XblMultiplayerSessionHandle sessionHandle,
                out XblDeviceToken[] deviceTokens)
            {
                int result;

                unsafe
                {
                    var tokenCount = new SizeT();
                    var tokenPtr = default(Interop.XblDeviceToken*);

                    result = Multiplayer.XblMultiplayerSessionHostCandidates(
                        sessionHandle.InteropHandle.handle,
                        &tokenPtr,
                        &tokenCount);

                    if (HR.SUCCEEDED(result))
                    {
                        deviceTokens = new XblDeviceToken[tokenCount.ToInt32()];
                        for (var i = 0; i < tokenCount.ToInt32(); i++)
                        {
                            deviceTokens[i] = new XblDeviceToken(*tokenPtr);
                            tokenPtr++;
                        }
                    }
                    else
                    {
                        deviceTokens = null;
                    }
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionSessionReference API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionsessionreference
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <returns>null if the session handle is invalid, non-null otherwise.</returns>
            public static XblMultiplayerSessionReference XblMultiplayerSessionSessionReference(
                XblMultiplayerSessionHandle sessionHandle)
            {
                XblMultiplayerSessionReference sessionReference = null;

                unsafe
                {
                    var sessionRefPtr = Multiplayer.XblMultiplayerSessionSessionReference(
                        sessionHandle.InteropHandle.handle);

                    if (sessionRefPtr != default(Interop.XblMultiplayerSessionReference*))
                    {
                        sessionReference = new XblMultiplayerSessionReference(*sessionRefPtr);
                    }
                }

                return sessionReference;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionSessionConstants API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionsessionconstants
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <returns>null if the session handle is invalid, non-null otherwise.</returns>
            public static XblMultiplayerSessionConstants XblMultiplayerSessionSessionConstants(
                XblMultiplayerSessionHandle sessionHandle)
            {
                XblMultiplayerSessionConstants sessionConstants = null;

                unsafe
                {
                    var result = Multiplayer.XblMultiplayerSessionSessionConstants(
                        sessionHandle.InteropHandle.handle);

                    if (result != default(Interop.XblMultiplayerSessionConstants*))
                    {
                        sessionConstants = new XblMultiplayerSessionConstants(*result);
                    }
                }

                return sessionConstants;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetMaxMembersInSession API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetmaxmembersinsession
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="maxMembersInSession"></param>
            public static void XblMultiplayerSessionConstantsSetMaxMembersInSession(
                XblMultiplayerSessionHandle sessionHandle,
                uint maxMembersInSession)
            {
                Multiplayer.XblMultiplayerSessionConstantsSetMaxMembersInSession(
                    sessionHandle.InteropHandle.handle, maxMembersInSession);
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetVisibility API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetvisibility
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="visibility"></param>
            public static void XblMultiplayerSessionConstantsSetVisibility(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerSessionVisibility visibility)
            {
                Multiplayer.XblMultiplayerSessionConstantsSetVisibility(
                    sessionHandle.InteropHandle.handle, visibility);
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetTimeouts API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssettimeouts
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="memberReservedTimeout"></param>
            /// <param name="memberInactiveTimeout"></param>
            /// <param name="memberReadyTimeout"></param>
            /// <param name="sessionEmptyTimeout"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetTimeouts(
                XblMultiplayerSessionHandle sessionHandle,
                TimeSpan memberReservedTimeout,
                TimeSpan memberInactiveTimeout,
                TimeSpan memberReadyTimeout,
                TimeSpan sessionEmptyTimeout)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetTimeouts(
                    sessionHandle.InteropHandle.handle,
                    Convert.ToUInt64(memberReservedTimeout.TotalMilliseconds),
                    Convert.ToUInt64(memberInactiveTimeout.TotalMilliseconds),
                    Convert.ToUInt64(memberReadyTimeout.TotalMilliseconds),
                    Convert.ToUInt64(sessionEmptyTimeout.TotalMilliseconds));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetArbitrationTimeouts API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetarbitrationtimeouts
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="arbitrationTimeout"></param>
            /// <param name="forfeitTimeout"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetArbitrationTimeouts(
                XblMultiplayerSessionHandle sessionHandle,
                TimeSpan arbitrationTimeout,
                TimeSpan forfeitTimeout)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetArbitrationTimeouts(
                    sessionHandle.InteropHandle.handle,
                    Convert.ToUInt64(arbitrationTimeout.TotalMilliseconds),
                    Convert.ToUInt64(forfeitTimeout.TotalMilliseconds));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetQosConnectivityMetrics API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetqosconnectivitymetrics
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="enableLatencyMetric"></param>
            /// <param name="enableBandwidthDownMetric"></param>
            /// <param name="enableBandwidthUpMetric"></param>
            /// <param name="enableCustomMetric"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetQosConnectivityMetrics(
                XblMultiplayerSessionHandle sessionHandle,
                bool enableLatencyMetric,
                bool enableBandwidthDownMetric,
                bool enableBandwidthUpMetric,
                bool enableCustomMetric)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetQosConnectivityMetrics(
                    sessionHandle.InteropHandle.handle,
                    Convert.ToByte(enableLatencyMetric),
                    Convert.ToByte(enableBandwidthDownMetric),
                    Convert.ToByte(enableBandwidthUpMetric),
                    Convert.ToByte(enableCustomMetric));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetMemberInitialization API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetmemberinitialization
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="memberInitialization"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetMemberInitialization(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerMemberInitialization memberInitialization)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetMemberInitialization(
                    sessionHandle.InteropHandle.handle,
                    new Interop.XblMultiplayerMemberInitialization(memberInitialization));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetPeerToPeerRequirements API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetpeertopeerrequirements
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="requirements"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetPeerToPeerRequirements(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerPeerToPeerRequirements requirements)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetPeerToPeerRequirements(
                    sessionHandle.InteropHandle.handle,
                    new Interop.XblMultiplayerPeerToPeerRequirements(requirements));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetPeerToHostRequirements API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetpeertohostrequirements
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="requirements"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetPeerToHostRequirements(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerPeerToHostRequirements requirements)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetPeerToHostRequirements(
                    sessionHandle.InteropHandle.handle,
                    new Interop.XblMultiplayerPeerToHostRequirements(requirements));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetmeasurementserveraddressesjson
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="measurementServerAddressesJson"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson(
                XblMultiplayerSessionHandle sessionHandle,
                string measurementServerAddressesJson)
            {
                int result;

                unsafe
                {
                    var requiredInteropLen = Converters.GetSizeRequiredToEncodeStringToUTF8(
                        measurementServerAddressesJson);
                    sbyte[] interopJson = new sbyte[requiredInteropLen];

                    fixed(sbyte* interopJsonPtr = &interopJson[0])
                    {
                        Converters.StringToNullTerminatedUTF8FixedPointer(
                            measurementServerAddressesJson, (byte*)interopJsonPtr, requiredInteropLen);
                        result = Multiplayer.XblMultiplayerSessionConstantsSetMeasurementServerAddressesJson(
                            sessionHandle.InteropHandle.handle,
                            interopJsonPtr);
                    }
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetCapabilities API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetcapabilities
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="capabilities"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetCapabilities(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerSessionCapabilities capabilities)
            {
                return Multiplayer.XblMultiplayerSessionConstantsSetCapabilities(
                    sessionHandle.InteropHandle.handle,
                    new Interop.XblMultiplayerSessionCapabilities(capabilities));
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionConstantsSetCloudComputePackageJson API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionconstantssetcloudcomputepackagejson
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="sessionCloudComputePackageConstantsJson"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionConstantsSetCloudComputePackageJson(
                XblMultiplayerSessionHandle sessionHandle,
                string sessionCloudComputePackageConstantsJson)
            {
                int result;

                unsafe
                {
                    var requiredInteropLen = Converters.GetSizeRequiredToEncodeStringToUTF8(
                        sessionCloudComputePackageConstantsJson);
                    sbyte[] interopJson = new sbyte[requiredInteropLen];

                    fixed (sbyte* interopJsonPtr = &interopJson[0])
                    {
                        Converters.StringToNullTerminatedUTF8FixedPointer(
                            sessionCloudComputePackageConstantsJson, (byte*)interopJsonPtr, requiredInteropLen);
                        result = Multiplayer.XblMultiplayerSessionConstantsSetCloudComputePackageJson(
                            sessionHandle.InteropHandle.handle,
                            interopJsonPtr);
                    }
                }

                return result;
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionPropertiesSetJoinRestriction API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionpropertiessetjoinrestriction
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="joinRestriction"></param>
            public static void XblMultiplayerSessionPropertiesSetJoinRestriction(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerSessionRestriction joinRestriction)
            {
                Multiplayer.XblMultiplayerSessionPropertiesSetJoinRestriction(
                    sessionHandle.InteropHandle.handle,
                    joinRestriction);
            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionPropertiesSetReadRestriction API:
            /// https://docs.microsoft.com/en-us/gaming/gdk/_content/gc/reference/live/xsapi-c/multiplayer_c/functions/xblmultiplayersessionpropertiessetreadrestriction
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="readRestriction"></param>
            public static void XblMultiplayerSessionPropertiesSetReadRestriction(
                XblMultiplayerSessionHandle sessionHandle,
                XblMultiplayerSessionRestriction readRestriction)
            {
                Multiplayer.XblMultiplayerSessionPropertiesSetReadRestriction(
                    sessionHandle.InteropHandle.handle,
                    readRestriction);
            }

            // TODO: place API method impls here (42 in ~510 mins [1 per ~12 min])

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionPropertiesSetKeywords API:
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="keywords"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionPropertiesSetKeywords(
                XblMultiplayerSessionHandle sessionHandle,
                string[] keywords)
            {

            }

            /// <summary>
            /// Wraps the underlying native XblMultiplayerSessionPropertiesSetTurnCollection API:
            /// </summary>
            /// <param name="sessionHandle"></param>
            /// <param name="turnCollectionMemberIds"></param>
            /// <returns>HR.S_OK on success, otherwise HR.FAILED(...) is true</returns>
            public static int XblMultiplayerSessionPropertiesSetTurnCollection(
                XblMultiplayerSessionHandle sessionHandle,
                uint[] turnCollectionMemberIds)
            {

            }

            // STOP HERE
            // TODO: place API method impls here (40 in ~??? mins [1 per ~?? min])

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionRoleTypes([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const XblMultiplayerRoleType **")] XblMultiplayerRoleType** roleTypes, [NativeTypeName("size_t *")] SizeT* roleTypesCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionGetRoleByName([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* roleTypeName, [NativeTypeName("const char *")] sbyte* roleName, [NativeTypeName("const XblMultiplayerRole **")] XblMultiplayerRole** role);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionSetMutableRoleSettings([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* roleTypeName, [NativeTypeName("const char *")] sbyte* roleName, [NativeTypeName("uint32_t *")] uint* maxMemberCount, [NativeTypeName("uint32_t *")] uint* targetMemberCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("const XblMultiplayerSessionMember *")]
            //public static extern XblMultiplayerSessionMember* XblMultiplayerSessionGetMember([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("uint32_t")] uint memberId);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("uint32_t")]
            //public static extern uint XblMultiplayerSessionMembersAccepted([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("const char *")]
            //public static extern sbyte* XblMultiplayerSessionRawServersJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionSetRawServersJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* rawServersJson);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("const char *")]
            //public static extern sbyte* XblMultiplayerSessionEtag([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("const XblMultiplayerSessionInfo *")]
            //public static extern XblMultiplayerSessionInfo* XblMultiplayerSessionGetInfo([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionAddMemberReservation([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("uint64_t")] ulong xuid, [NativeTypeName("const char *")] sbyte* memberCustomConstantsJson, [NativeTypeName("bool")] byte initializeRequested);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //public static extern void XblMultiplayerSessionSetInitializationSucceeded([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("bool")] byte initializationSucceeded);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //public static extern void XblMultiplayerSessionSetMatchmakingServerConnectionPath([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* serverConnectionPath);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //public static extern void XblMultiplayerSessionSetLocked([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("bool")] byte locked);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //public static extern void XblMultiplayerSessionSetAllocateCloudCompute([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("bool")] byte allocateCloudCompute);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //public static extern void XblMultiplayerSessionSetMatchmakingResubmit([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("bool")] byte matchResubmit);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionSetServerConnectionStringCandidates([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char **")] sbyte** serverConnectionStringCandidates, [NativeTypeName("size_t")] SizeT serverConnectionStringCandidatesCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserSetRoles([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const XblMultiplayerSessionMemberRole *")] XblMultiplayerSessionMemberRole* roles, [NativeTypeName("size_t")] SizeT rolesCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserSetMembersInGroup([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr session, [NativeTypeName("uint32_t *")] uint* memberIds, [NativeTypeName("size_t")] SizeT memberIdsCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserSetGroups([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char **")] sbyte** groups, [NativeTypeName("size_t")] SizeT groupsCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserSetEncounters([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char **")] sbyte** encounters, [NativeTypeName("size_t")] SizeT encountersCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserSetQosMeasurements([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* measurements);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserSetCustomPropertyJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* valueJson);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionCurrentUserDeleteCustomPropertyJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* name);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionSetMatchmakingTargetSessionConstantsJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* matchmakingTargetSessionConstantsJson);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionSetCustomPropertyJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* valueJson);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSessionDeleteCustomPropertyJson([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr handle, [NativeTypeName("const char *")] sbyte* name);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //public static extern XblMultiplayerSessionChangeTypes XblMultiplayerSessionCompare([NativeTypeName("XblMultiplayerSessionHandle")] IntPtr currentSessionHandle, [NativeTypeName("XblMultiplayerSessionHandle")] IntPtr oldSessionHandle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerWriteSessionByHandleAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("XblMultiplayerSessionHandle")] IntPtr multiplayerSession, XblMultiplayerSessionWriteMode writeMode, [NativeTypeName("const char *")] sbyte* handleId, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerWriteSessionByHandleResult([NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async, [NativeTypeName("XblMultiplayerSessionHandle *")] IntPtr* handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerGetSessionAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("const XblMultiplayerSessionReference *")] XblMultiplayerSessionReference* sessionReference, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerGetSessionResult([NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async, [NativeTypeName("XblMultiplayerSessionHandle *")] IntPtr* handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerGetSessionByHandleAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("const char *")] sbyte* handleId, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerGetSessionByHandleResult([NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async, [NativeTypeName("XblMultiplayerSessionHandle *")] IntPtr* handle);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerQuerySessionsAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("const XblMultiplayerSessionQuery *")] XblMultiplayerSessionQuery* sessionQuery, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerQuerySessionsResultCount([NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async, [NativeTypeName("size_t *")] SizeT* sessionCount);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerQuerySessionsResult([NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async, [NativeTypeName("size_t")] SizeT sessionCount, XblMultiplayerSessionQueryResult* sessions);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSetActivityAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("const XblMultiplayerSessionReference *")] XblMultiplayerSessionReference* sessionReference, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerClearActivityAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("const char *")] sbyte* scid, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSendInvitesAsync([NativeTypeName("XblContextHandle")] IntPtr xblContext, [NativeTypeName("const XblMultiplayerSessionReference *")] XblMultiplayerSessionReference* sessionReference, [NativeTypeName("const uint64_t *")] ulong* xuids, [NativeTypeName("size_t")] SizeT xuidsCount, [NativeTypeName("uint32_t")] uint titleId, [NativeTypeName("const char *")] sbyte* contextStringId, [NativeTypeName("const char *")] sbyte* customActivationContext, [NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async);

            //[DllImport("Microsoft_Xbox_Services_141_GDK_C_Thunks", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            //[return: NativeTypeName("HRESULT")]
            //public static extern int XblMultiplayerSendInvitesResult([NativeTypeName("XAsyncBlock *")] XAsyncBlockPtr async, [NativeTypeName("size_t")] SizeT handlesCount, XblMultiplayerInviteHandle* handles);
        }
    }
}