package crc642007020c1aa00a24;


public class AppointmentsViewPagerAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ZAPP.Adapters.AppointmentsViewPagerAdapterViewHolder, ZAPP", AppointmentsViewPagerAdapterViewHolder.class, __md_methods);
	}


	public AppointmentsViewPagerAdapterViewHolder ()
	{
		super ();
		if (getClass () == AppointmentsViewPagerAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("ZAPP.Adapters.AppointmentsViewPagerAdapterViewHolder, ZAPP", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
