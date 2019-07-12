using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

#region Prepared Code
	public void Trigger()
	{
		GetComponent<Animator>().SetBool("active", true);
	}
#endregion

}
