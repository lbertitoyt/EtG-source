public enum AkMonitorErrorCode
{
	ErrorCode_NoError,
	ErrorCode_FileNotFound,
	ErrorCode_CannotOpenFile,
	ErrorCode_CannotStartStreamNoMemory,
	ErrorCode_IODevice,
	ErrorCode_IncompatibleIOSettings,
	ErrorCode_PluginUnsupportedChannelConfiguration,
	ErrorCode_PluginMediaUnavailable,
	ErrorCode_PluginInitialisationFailed,
	ErrorCode_PluginProcessingFailed,
	ErrorCode_PluginExecutionInvalid,
	ErrorCode_PluginAllocationFailed,
	ErrorCode_VorbisRequireSeekTable,
	ErrorCode_VorbisDecodeError,
	ErrorCode_AACDecodeError,
	ErrorCode_xWMACreateDecoderFailed,
	ErrorCode_ATRAC9CreateDecoderFailed,
	ErrorCode_ATRAC9CreateDecoderFailedChShortage,
	ErrorCode_ATRAC9DecodeFailed,
	ErrorCode_ATRAC9ClearContextFailed,
	ErrorCode_ATRAC9LoopSectionTooSmall,
	ErrorCode_InvalidAudioFileHeader,
	ErrorCode_AudioFileHeaderTooLarge,
	ErrorCode_FileTooSmall,
	ErrorCode_TransitionNotAccurateChannel,
	ErrorCode_TransitionNotAccurateStarvation,
	ErrorCode_NothingToPlay,
	ErrorCode_PlayFailed,
	ErrorCode_StingerCouldNotBeScheduled,
	ErrorCode_TooLongSegmentLookAhead,
	ErrorCode_CannotScheduleMusicSwitch,
	ErrorCode_TooManySimultaneousMusicSegments,
	ErrorCode_PlaylistStoppedForEditing,
	ErrorCode_MusicClipsRescheduledAfterTrackEdit,
	ErrorCode_CannotPlaySource_Create,
	ErrorCode_CannotPlaySource_VirtualOff,
	ErrorCode_CannotPlaySource_TimeSkip,
	ErrorCode_CannotPlaySource_InconsistentState,
	ErrorCode_MediaNotLoaded,
	ErrorCode_VoiceStarving,
	ErrorCode_StreamingSourceStarving,
	ErrorCode_XMADecoderSourceStarving,
	ErrorCode_XMADecodingError,
	ErrorCode_InvalidXMAData,
	ErrorCode_PluginNotRegistered,
	ErrorCode_CodecNotRegistered,
	ErrorCode_PluginVersionMismatch,
	ErrorCode_EventIDNotFound,
	ErrorCode_InvalidGroupID,
	ErrorCode_SelectedChildNotAvailable,
	ErrorCode_SelectedNodeNotAvailable,
	ErrorCode_SelectedMediaNotAvailable,
	ErrorCode_NoValidSwitch,
	ErrorCode_SelectedNodeNotAvailablePlay,
	ErrorCode_FeedbackVoiceStarving,
	ErrorCode_BankLoadFailed,
	ErrorCode_BankUnloadFailed,
	ErrorCode_ErrorWhileLoadingBank,
	ErrorCode_InsufficientSpaceToLoadBank,
	ErrorCode_LowerEngineCommandListFull,
	ErrorCode_SeekNoMarker,
	ErrorCode_CannotSeekContinuous,
	ErrorCode_SeekAfterEof,
	ErrorCode_UnknownGameObject,
	ErrorCode_UnknownEmitter,
	ErrorCode_UnknownListener,
	ErrorCode_GameObjectIsNotListener,
	ErrorCode_GameObjectIsNotEmitter,
	ErrorCode_UnknownGameObjectEvent,
	ErrorCode_GameObjectIsNotEmitterEvent,
	ErrorCode_ExternalSourceNotResolved,
	ErrorCode_FileFormatMismatch,
	ErrorCode_CommandQueueFull,
	ErrorCode_CommandTooLarge,
	ErrorCode_XMACreateDecoderLimitReached,
	ErrorCode_XMAStreamBufferTooSmall,
	ErrorCode_ModulatorScopeError_Inst,
	ErrorCode_ModulatorScopeError_Obj,
	ErrorCode_SeekAfterEndOfPlaylist,
	ErrorCode_OpusRequireSeekTable,
	ErrorCode_OpusDecodeError,
	ErrorCode_OpusCreateDecoderFailed,
	ErrorCode_SourcePluginNotFound,
	ErrorCode_VirtualVoiceLimit,
	ErrorCode_AudioDeviceShareSetNotFound,
	ErrorCode_NotEnoughMemoryToStart,
	Num_ErrorCodes
}
