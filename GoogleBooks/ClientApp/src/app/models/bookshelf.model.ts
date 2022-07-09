import { Volume } from "./volume.model"

export interface Bookshelf {
    userId: string,
    volumes: Volume[],
    id: number,
    title: string,
    access: string,
    updated: Date,
    created: Date,
    volumeCount: number,
    volumesLastUpdated: Date
}
