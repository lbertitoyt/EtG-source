using System;
using System.Runtime.InteropServices;

public class AkTriangleArray : IDisposable
{
	private readonly int SIZE_OF_AKTRIANGLE = AkSoundEnginePINVOKE.CSharp_AkTriangleProxy_GetSizeOf();

	private IntPtr m_Buffer;

	private int m_Count;

	public AkTriangleArray(int count)
	{
		m_Count = count;
		m_Buffer = Marshal.AllocHGlobal(count * SIZE_OF_AKTRIANGLE);
		if (m_Buffer != IntPtr.Zero)
		{
			for (int i = 0; i < count; i++)
			{
				AkSoundEnginePINVOKE.CSharp_AkTriangleProxy_Clear(GetObjectPtr(i));
			}
		}
	}

	public void Dispose()
	{
		if (m_Buffer != IntPtr.Zero)
		{
			for (int i = 0; i < m_Count; i++)
			{
				AkSoundEnginePINVOKE.CSharp_AkTriangleProxy_DeleteName(GetObjectPtr(i));
			}
			Marshal.FreeHGlobal(m_Buffer);
			m_Buffer = IntPtr.Zero;
			m_Count = 0;
		}
	}

	~AkTriangleArray()
	{
		Dispose();
	}

	public void Reset()
	{
		m_Count = 0;
	}

	public AkTriangle GetTriangle(int index)
	{
		if (index >= m_Count)
		{
			return null;
		}
		return new AkTriangle(GetObjectPtr(index), false);
	}

	public IntPtr GetBuffer()
	{
		return m_Buffer;
	}

	public int Count()
	{
		return m_Count;
	}

	private IntPtr GetObjectPtr(int index)
	{
		return (IntPtr)(m_Buffer.ToInt64() + SIZE_OF_AKTRIANGLE * index);
	}
}
