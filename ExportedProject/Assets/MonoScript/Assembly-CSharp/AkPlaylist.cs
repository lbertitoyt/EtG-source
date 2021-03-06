using System;

public class AkPlaylist : AkPlaylistArray
{
	private IntPtr swigCPtr;

	internal AkPlaylist(IntPtr cPtr, bool cMemoryOwn)
		: base(AkSoundEnginePINVOKE.CSharp_AkPlaylist_SWIGUpcast(cPtr), cMemoryOwn)
	{
		swigCPtr = cPtr;
	}

	public AkPlaylist()
		: this(AkSoundEnginePINVOKE.CSharp_new_AkPlaylist(), true)
	{
	}

	internal static IntPtr getCPtr(AkPlaylist obj)
	{
		return (obj != null) ? obj.swigCPtr : IntPtr.Zero;
	}

	internal override void setCPtr(IntPtr cPtr)
	{
		base.setCPtr(AkSoundEnginePINVOKE.CSharp_AkPlaylist_SWIGUpcast(cPtr));
		swigCPtr = cPtr;
	}

	~AkPlaylist()
	{
		Dispose();
	}

	public override void Dispose()
	{
		lock (this)
		{
			if (swigCPtr != IntPtr.Zero)
			{
				if (swigCMemOwn)
				{
					swigCMemOwn = false;
					AkSoundEnginePINVOKE.CSharp_delete_AkPlaylist(swigCPtr);
				}
				swigCPtr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
			base.Dispose();
		}
	}

	public AKRESULT Enqueue(uint in_audioNodeID, int in_msDelay, IntPtr in_pCustomInfo, uint in_cExternals, AkExternalSourceInfo in_pExternalSources)
	{
		return (AKRESULT)AkSoundEnginePINVOKE.CSharp_AkPlaylist_Enqueue__SWIG_0(swigCPtr, in_audioNodeID, in_msDelay, in_pCustomInfo, in_cExternals, AkExternalSourceInfo.getCPtr(in_pExternalSources));
	}

	public AKRESULT Enqueue(uint in_audioNodeID, int in_msDelay, IntPtr in_pCustomInfo, uint in_cExternals)
	{
		return (AKRESULT)AkSoundEnginePINVOKE.CSharp_AkPlaylist_Enqueue__SWIG_1(swigCPtr, in_audioNodeID, in_msDelay, in_pCustomInfo, in_cExternals);
	}

	public AKRESULT Enqueue(uint in_audioNodeID, int in_msDelay, IntPtr in_pCustomInfo)
	{
		return (AKRESULT)AkSoundEnginePINVOKE.CSharp_AkPlaylist_Enqueue__SWIG_2(swigCPtr, in_audioNodeID, in_msDelay, in_pCustomInfo);
	}

	public AKRESULT Enqueue(uint in_audioNodeID, int in_msDelay)
	{
		return (AKRESULT)AkSoundEnginePINVOKE.CSharp_AkPlaylist_Enqueue__SWIG_3(swigCPtr, in_audioNodeID, in_msDelay);
	}

	public AKRESULT Enqueue(uint in_audioNodeID)
	{
		return (AKRESULT)AkSoundEnginePINVOKE.CSharp_AkPlaylist_Enqueue__SWIG_4(swigCPtr, in_audioNodeID);
	}
}
