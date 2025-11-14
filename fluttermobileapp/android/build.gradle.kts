allprojects {
    repositories {
        google()
        mavenCentral()
    }
}

plugins {
    id("org.jetbrains.kotlin.android") version "1.9.20" apply false
    id("com.android.application") version "8.7.3" apply false
}

val newBuildDir: Directory = rootProject.layout.buildDirectory.dir("../../build").get()
rootProject.layout.buildDirectory.value(newBuildDir)

subprojects {
    val newSubprojectBuildDir: Directory = newBuildDir.dir(project.name)
    project.layout.buildDirectory.value(newSubprojectBuildDir)

    project.evaluationDependsOn(":app")
}

// 각 subproject에 대해 dependencies 블록 추가
dependencies {
    //implementation("org.jetbrains.kotlin:kotlin-stdlib:1.9.20")
}

