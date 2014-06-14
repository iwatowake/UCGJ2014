using UnityEngine;
using System.Collections;

public interface UnityChanCollisionInterface {
	void OnCollDamage(int damage);
	void OnCollPararise(float sec);
	void OnCollImpact(Vector3 pow);
}
