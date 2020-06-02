using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
	public class BossAIController : MonoBehaviour
	{
		public int x = 0;
		public int y = 0;
		// public string xdirection = "right";
		// public string ydirection = "up";

		public Constants.DIRECTIONS xdirection;
		public Constants.DIRECTIONS ydirection;

		public void Start()
		{
			x = 0;
			y = 0;
		}

		public void BackAndForth()
		{
			if(x > 5)
				xdirection = Constants.DIRECTIONS.LEFT;
			if(x < -5)
				xdirection = Constants.DIRECTIONS.RIGHT;

			switch(xdirection)
			{
				case Constants.DIRECTIONS.RIGHT:
					this.transform.Translate(Vector3.right);
					x += 1;
					return;
				case Constants.DIRECTIONS.LEFT:
					this.transform.Translate(Vector3.left);
					x += -1;
					return;
			}
		}

		public void UpAndDown()
		{
			if(y > 5)
				ydirection = Constants.DIRECTIONS.DOWN;
			if(y < -5)
				ydirection = Constants.DIRECTIONS.UP;

			switch(ydirection)
			{
				case Constants.DIRECTIONS.UP:
					this.transform.Translate(Vector3.up);
					y += 1;
					return;
				case Constants.DIRECTIONS.DOWN:
					this.transform.Translate(Vector3.down);
					y += -1;
					return;
			}
		}

	}
}