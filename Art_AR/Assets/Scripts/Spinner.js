var target : Transform;
var spin_speed : int;
var spin_direction : boolean;

function spinner() {
       if (spin_direction) {
       		transform.Rotate(0,spin_speed*Time.deltaTime,0);
       		}
       else {
       		transform.Rotate(0,-spin_speed*Time.deltaTime,0);
       		}
}

function Update() {
      spinner();
}