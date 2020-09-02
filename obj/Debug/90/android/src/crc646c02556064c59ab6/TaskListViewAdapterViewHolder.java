package crc646c02556064c59ab6;


public class TaskListViewAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ZAPP.TaskListViewAdapterViewHolder, ZAPP", TaskListViewAdapterViewHolder.class, __md_methods);
	}


	public TaskListViewAdapterViewHolder ()
	{
		super ();
		if (getClass () == TaskListViewAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("ZAPP.TaskListViewAdapterViewHolder, ZAPP", "", this, new java.lang.Object[] {  });
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
