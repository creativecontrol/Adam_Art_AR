
function FixedUpdate () {
	MoveHero();
	JumpHero();
}

function PlayAnimation(AnimName : String) {
	if (!animation.IsPlaying(AnimName))
	animation.CrossFadeQueued(AnimName, 0.3, QueueMode.PlayNow);
}

function CheckForIdle() {
	if (animation.IsPlaying("run")) PlayAnimation("idle");
	if (!animation.isPlaying) animation.Play("idle");
}

function MoveHero() {
	if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2) {
		if (Input.GetAxis("Horizontal") > 0.02) transform.eulerAngles.y = -90;
		else if (Input.GetAxis("Horizontal") < -0.02) transform.eulerAngles.y = 90;
		transform.Translate(Vector3.forward * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime * 3.5);
		if (!animation.IsPlaying("jump")) PlayAnimation("run");
	}
	else CheckForIdle();
}

private var nextJump : float;

function JumpHero () {
	if (Input.GetButton("Jump") && nextJump < Time.time) {
		rigidbody.AddForce(Vector3.up * 25000);
		PlayAnimation("jump");
		nextJump = Time.time + 1;
		yield WaitForSeconds(0.7); PlayAnimation("idle");
	}
}

function OnCollisionEnter(collision : Collision) {
	for ( var contact : ContactPoint in collision.contacts ) {
		if (contact.otherCollider.name == "GameFinish")
		Application.LoadLevel("gameFinish");
	}
}