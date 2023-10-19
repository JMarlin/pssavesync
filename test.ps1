function getSaveDetails([string] $cardPath) {

	Invoke-RestMethod `
		-Method POST `
		-Uri "https://localhost:7084/Saves" `
		-Body "`"$([Convert]::ToBase64String([System.IO.File]::ReadAllBytes($cardPath)))`"" `
		-ContentType "application/json" `
		-SkipCertificateCheck
}

function dumpAllSaveData() {
	ls /Volumes/Untitled/PSP/SAVEDATA/SLUS0*/SCEVMC*.VMP `
	| % { getSaveDetails $_ }
}
